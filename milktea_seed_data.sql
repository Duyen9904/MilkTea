-- Check if the database exists and drop it if it does

USE [master];
GO

IF EXISTS (SELECT name FROM master.sys.databases WHERE name = N'MilkTeaShop')
BEGIN
    -- Ensure no connections are active
    ALTER DATABASE [MilkTeaShop] SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE [MilkTeaShop];
END
GO

-- Create Database
CREATE DATABASE [MilkTeaShop];
GO

USE [MilkTeaShop];
GO

-- Create Tables
CREATE TABLE [Account] (
    [account_id] INT IDENTITY(1,1) PRIMARY KEY,
    [email] VARCHAR(255) NOT NULL,
    [password] VARCHAR(255) NOT NULL,
    [first_name] NVARCHAR(255) NOT NULL,
    [last_name] NVARCHAR(255) NOT NULL,
    [phone] VARCHAR(20) NOT NULL,
    [address] NVARCHAR(255) NOT NULL,
    [role] VARCHAR(10) NOT NULL CHECK ([role] IN ('ADMIN', 'CUSTOMER', 'STAFF')),
    [is_active] BIT DEFAULT 1 NOT NULL,
    [created_at] DATETIME2(6) DEFAULT GETDATE() NOT NULL,
    CONSTRAINT [UQ_Account_Email] UNIQUE ([email])
);

CREATE TABLE [Category] (
    [category_id] INT IDENTITY(1,1) PRIMARY KEY,
    [category_name] NVARCHAR(50) NOT NULL,
    [description] NVARCHAR(255) NULL
);

-- MilkTeaProduct table
CREATE TABLE [MilkTeaProduct] (
    [product_id] INT IDENTITY(1,1) PRIMARY KEY,
    [product_name] NVARCHAR(100) NOT NULL,
    [category_id] INT NOT NULL FOREIGN KEY REFERENCES [Category]([category_id]),
    [description] NVARCHAR(500) NULL,
    [base_price] MONEY NOT NULL,
    [image_path] NVARCHAR(255) NULL,
    [is_available] BIT DEFAULT 1 NOT NULL,
    [created_date] DATETIME DEFAULT GETDATE() NOT NULL,
    [last_modified] DATETIME DEFAULT GETDATE() NOT NULL
);

-- Sizes for MilkTeaProducts
CREATE TABLE [Size] (
    [size_id] INT IDENTITY(1,1) PRIMARY KEY,
    [size_name] NVARCHAR(20) NOT NULL,
    [price_modifier] MONEY DEFAULT 0 NOT NULL
);

-- ProductSizes join table
CREATE TABLE [ProductSize] (
    [product_size_id] INT IDENTITY(1,1) PRIMARY KEY,
    [product_id] INT NOT NULL FOREIGN KEY REFERENCES [MilkTeaProduct]([product_id]),
    [size_id] INT NOT NULL FOREIGN KEY REFERENCES [Size]([size_id]),
    [price] MONEY NOT NULL,
    CONSTRAINT [UQ_ProductSize] UNIQUE ([product_id], [size_id])
);

-- Toppings 
CREATE TABLE [Topping] (
    [topping_id] INT IDENTITY(1,1) PRIMARY KEY,
    [topping_name] NVARCHAR(50) NOT NULL,
    [description] NVARCHAR(255) NULL,
    [price] MONEY NOT NULL,
    [image_path] NVARCHAR(255) NULL,
    [is_available] BIT DEFAULT 1 NOT NULL
);

-- Orders
CREATE TABLE [Order] (
    [order_id] INT IDENTITY(1,1) PRIMARY KEY,
    [account_id] INT NOT NULL FOREIGN KEY REFERENCES [Account]([account_id]),
    [order_date] DATETIME DEFAULT GETDATE() NOT NULL,
    [subtotal] MONEY NOT NULL,
    [tax] MONEY NOT NULL,
    [delivery_fee] MONEY DEFAULT 0 NOT NULL,
    [total_amount] MONEY NOT NULL,
    [status] NVARCHAR(20) DEFAULT 'Pending' NOT NULL CHECK ([status] IN ('Pending', 'Processing', 'Completed', 'Delivered', 'Cancelled')),
    [payment_method] NVARCHAR(20) NOT NULL CHECK ([payment_method] IN ('Cash', 'Credit Card', 'Debit Card', 'Mobile Payment')),
    [payment_status] NVARCHAR(20) DEFAULT 'Pending' NOT NULL CHECK ([payment_status] IN ('Pending', 'Completed', 'Failed', 'Refunded')),
    [delivery_address] NVARCHAR(255) NOT NULL,
    [notes] NVARCHAR(500) NULL,
    [processed_by] INT NULL FOREIGN KEY REFERENCES [Account]([account_id])
);

-- Order Items
CREATE TABLE [OrderItem] (
    [order_item_id] INT IDENTITY(1,1) PRIMARY KEY,
    [order_id] INT NOT NULL FOREIGN KEY REFERENCES [Order]([order_id]),
    [product_size_id] INT NOT NULL FOREIGN KEY REFERENCES [ProductSize]([product_size_id]),
    [quantity] INT DEFAULT 1 NOT NULL,
    [special_instructions] NVARCHAR(255) NULL,
    [unit_price] MONEY NOT NULL,
    [subtotal] AS ([quantity] * [unit_price]) PERSISTED
);

-- OrderItemToppings junction table
CREATE TABLE [OrderItemTopping] (
    [order_item_topping_id] INT IDENTITY(1,1) PRIMARY KEY,
    [order_item_id] INT NOT NULL FOREIGN KEY REFERENCES [OrderItem]([order_item_id]),
    [topping_id] INT NOT NULL FOREIGN KEY REFERENCES [Topping]([topping_id]),
    [price] MONEY NOT NULL,
    CONSTRAINT [UQ_OrderItemTopping] UNIQUE ([order_item_id], [topping_id])
);



-- Transaction History (Self-contained, no foreign keys)
CREATE TABLE [Transaction] (
    [transaction_id] INT IDENTITY(1,1) PRIMARY KEY,
    [account_id] INT NOT NULL, -- Store as plain integer, no FK
    [amount] MONEY NOT NULL,
    [transaction_type] NVARCHAR(20) NOT NULL CHECK ([transaction_type] IN ('Payment', 'Refund', 'Adjustment', 'Deposit')),
    [description] NVARCHAR(255) NULL,
    [order_id] INT NULL, -- Store as plain integer, no FK
    [transaction_date] DATETIME NOT NULL DEFAULT GETDATE(),
    [processed_by] INT NULL -- Store as plain integer, no FK
);




-- Combo Table
CREATE TABLE [Combo] (
    [combo_id] INT IDENTITY(1,1) PRIMARY KEY,
    [combo_name] NVARCHAR(100) NOT NULL,
    [description] NVARCHAR(500) NULL,
    [total_price] MONEY NOT NULL,
    [image_path] NVARCHAR(255) NULL,
    [is_available] BIT DEFAULT 1 NOT NULL,
    [created_at] DATETIME2(6) DEFAULT GETDATE() NOT NULL
);

-- Combo Items (linking to ProductSize instead of Product + Size separately)
CREATE TABLE [ComboItem] (
    [combo_item_id] INT IDENTITY(1,1) PRIMARY KEY,
    [combo_id] INT NOT NULL FOREIGN KEY REFERENCES [Combo]([combo_id]),
    [product_size_id] INT NOT NULL FOREIGN KEY REFERENCES [ProductSize]([product_size_id]),
    [quantity] INT DEFAULT 1 NOT NULL,
    CONSTRAINT [UQ_ComboItem] UNIQUE ([combo_id], [product_size_id])
);

-- Order Combos (Combos included in orders)
CREATE TABLE [OrderCombo] (
    [order_combo_id] INT IDENTITY(1,1) PRIMARY KEY,
    [order_id] INT NOT NULL FOREIGN KEY REFERENCES [Order]([order_id]),
    [combo_id] INT NOT NULL FOREIGN KEY REFERENCES [Combo]([combo_id]),
    [quantity] INT DEFAULT 1 NOT NULL,
    [unit_price] MONEY NOT NULL,
    CONSTRAINT [UQ_OrderCombo] UNIQUE ([order_id], [combo_id])
);



USE [MilkTeaShop];
GO

-- Seed data for Account table (with Vietnamese names and phone formats)
INSERT INTO [Account] ([email], [password], [first_name], [last_name], [phone], [address], [role])
VALUES 
    ('admin@trademilktea.vn', 'hashed_password_123', N'Nguyễn', N'Quản Trị', '0912345678', N'Số 123 Đường Quản Trị, Quận 1, TP. Hồ Chí Minh', 'ADMIN'),
    ('staff1@trademilktea.vn', 'hashed_password_456', N'Trần', N'Văn Nhân', '0923456789', N'Số 456 Đường Nhân Viên, Quận Tân Bình, TP. Hồ Chí Minh', 'STAFF'),
    ('staff2@trademilktea.vn', 'hashed_password_789', N'Lê', N'Thị Nhân Viên', '0934567890', N'Số 789 Đường Nhân Viên, Quận Gò Vấp, TP. Hồ Chí Minh', 'STAFF'),
    ('customer1@example.vn', 'hashed_password_abc', N'Phạm', N'Văn Khách', '0945678901', N'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer2@example.vn', 'hashed_password_def', N'Võ', N'Thị Khách', '0956789012', N'Số 202 Đường Khách Hàng, Quận Bình Thạnh, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer3@example.vn', 'hashed_password_ghi', N'Hoàng', N'Văn Mua', '0967890123', N'Số 303 Đường Khách Hàng, Quận 5, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer4@example.vn', 'hashed_password_jkl', N'Đỗ', N'Minh Khách', '0978901234', N'Số 404 Đường Khách Hàng, Quận 10, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer5@example.vn', 'hashed_password_mno', N'Ngô', N'Thị Mua Sắm', '0989012345', N'Số 505 Đường Khách Hàng, Quận Phú Nhuận, TP. Hồ Chí Minh', 'CUSTOMER');

-- Keep Category seed data the same
INSERT INTO [Category] ([category_name], [description])
VALUES 
    (N'Trà Trái Cây', N'Trà tươi mát được chiết xuất từ các loại trái cây tự nhiên'),
    (N'Trà Sữa', N'Trà truyền thống kết hợp với sữa và các topping'),
    (N'Đồ Uống Đặc Biệt', N'Các loại đồ uống đặc chế và mùa vụ'),
    (N'Cà Phê', N'Các loại cà phê cao cấp với nhiều tuỳ chọn'),
    (N'Nước Đá Xay', N'Đồ uống đá xay mát lạnh cho những ngày nóng');

-- Update MilkTeaProduct with Vietnamese names and descriptions
INSERT INTO [MilkTeaProduct] ([product_name], [category_id], [description], [base_price], [image_path], [is_available])
VALUES 
    (N'Trà Sữa Truyền Thống', 2, N'Trà sữa đen kinh điển', 45000, 'https://liberico.vn/wp-content/uploads/2022/02/2-0a04.png', 1),
    (N'Trà Sữa Khoai Môn', 2, N'Trà sữa khoai môn béo ngậy', 47500, 'https://viettuantea.vn/wp-content/uploads/2020/11/cong-thuc-pha-tra-sua-khoai-mon.jpg', 1),
    (N'Trà Sữa Đường Nâu', 2, N'Trà sữa với siro đường nâu và trân châu', 52500, 'https://channel.mediacdn.vn/2018/12/20/photo-1-1545307248459610816374.jpg', 1),
    (N'Trà Xanh Xoài', 1, N'Trà xanh thơm mát với vị xoài', 45000, 'https://tralocphat.com.vn/wp-content/uploads/2024/11/87-1024x1024.png', 1),
    (N'Trà Vải', 1, N'Trà đen với hương vị trái vải', 45000, 'https://cdn.tgdd.vn/Files/2020/04/15/1249146/3-cach-lam-tra-vai-va-hong-tra-vai-don-gian-ta-nha.jpg', 1),
    (N'Trà Dâu', 1, N'Trà tươi mát với hương dâu', 45000, 'https://bizweb.dktcdn.net/100/290/576/files/foody-upload-api-foody-mobile-tr-8afca038-210529163258.jpg?v=1625117866947', 1),
    (N'Matcha Sữa', 3, N'Matcha cao cấp kết hợp sữa', 55000, 'https://file.hstatic.net/1000135323/article/tra_sua_ngon_1ebddd76eed14d708abc4a9ee597b304.jpg', 1),
    (N'Cà Phê Việt Nam', 4, N'Cà phê đậm đà với sữa đặc', 47500, 'https://file.hstatic.net/1000135323/article/tra_sua_ngon_1ebddd76eed14d708abc4a9ee597b304.jpg', 1),
    (N'Xoài Đá Xay', 5, N'Đá xay mát lạnh vị xoài', 50000, 'https://file.hstatic.net/1000135323/article/tra_sua_ngon_1ebddd76eed14d708abc4a9ee597b304.jpg', 1),
    (N'Trà Thái', 2, N'Trà Thái truyền thống', 47500, 'https://file.hstatic.net/1000135323/article/tra_sua_ngon_1ebddd76eed14d708abc4a9ee597b304.jpg', 1),
    (N'Trà Chanh Mật Ong', 1, N'Trà đen với mật ong và chanh tươi', 42500, 'https://file.hstatic.net/1000135323/article/tra_sua_ngon_1ebddd76eed14d708abc4a9ee597b304.jpg', 1),
    (N'Trà Sữa Oreo', 3, N'Trà sữa kết hợp bánh quy Oreo', 57500, 'https://file.hstatic.net/1000135323/article/tra_sua_ngon_1ebddd76eed14d708abc4a9ee597b304.jpg', 1);

-- Size table remains the same
INSERT INTO [Size] ([size_name], [price_modifier])
VALUES 
    (N'Nhỏ', 0.00),
    (N'Vừa', 7500),
    (N'Lớn', 15000);

-- ProductSize remains the same logic
INSERT INTO [ProductSize] ([product_id], [size_id], [price])
SELECT 
    p.[product_id], 
    s.[size_id], 
    p.[base_price] + s.[price_modifier] AS [price]
FROM 
    [MilkTeaProduct] p
CROSS JOIN 
    [Size] s;

-- Topping table with Vietnamese names
INSERT INTO [Topping] ([topping_name], [description], [price], [image_path], [is_available])
VALUES 
    (N'Trân Châu', N'Trân châu truyền thống', 7500, 'boba_pearls.jpg', 1),
    (N'Thạch Cỏ Biển', N'Thạch ngọt từ rong biển', 7500, 'grass_jelly.jpg', 1),
    (N'Nha Đam', N'Viên nha đam ngọt ngào', 7500, 'aloe_vera.jpg', 1),
    ('Pudding', N'Pudding trứng mịn', 7500, 'pudding.jpg', 1),
    (N'Trân Châu Trong', N'Trân châu trong suốt', 7500, 'crystal_boba.jpg', 1),
    (N'Sữa Tươi', N'Sữa tươi nguyên chất', 5000, 'fresh_milk.jpg', 1),
    ('Kem Cheese', N'Lớp kem cheese mặn ngọt', 10000, 'cream_cheese.jpg', 1),
    (N'Thạch Cà Phê', N'Thạch vị cà phê', 7500, 'coffee_jelly.jpg', 1),
    (N'Thạch Vải', N'Thạch vị trái vải', 7500, 'lychee_jelly.jpg', 1),
    (N'Trái Cây Tươi', N'Các miếng trái cây tươi', 12500, 'fresh_fruit.jpg', 1);

-- Updated Order data with Vietnamese-style pricing and details
INSERT INTO [Order] ([account_id], [order_date], [subtotal], [tax], [delivery_fee], [total_amount], [status], [payment_method], [payment_status], [delivery_address], [notes], [processed_by])
VALUES 
    (4, DATEADD(DAY, -7, GETDATE()), 102500, 8200, 20000, 130700, 'Completed', 'Credit Card', 'Completed', N'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh', NULL, 2),
    (5, DATEADD(DAY, -5, GETDATE()), 157500, 12600, 20000, 190100, 'Completed', 'Debit Card', 'Completed', N'Số 202 Đường Khách Hàng, Quận Bình Thạnh, TP. Hồ Chí Minh', N'Ít đá', 2),
    (6, DATEADD(DAY, -3, GETDATE()), 210000, 16800, 20000, 246800, 'Delivered', 'Mobile Payment', 'Completed', N'Số 303 Đường Khách Hàng, Quận 5, TP. Hồ Chí Minh', NULL, 3),
    (7, DATEADD(DAY, -1, GETDATE()), 115000, 9200, 20000, 144200, 'Delivered', 'Credit Card', 'Completed', N'Số 404 Đường Khách Hàng, Quận 10, TP. Hồ Chí Minh', N'Gọi chuông cửa', 3),
    (8, GETDATE(), 172500, 13800, 20000, 206300, 'Processing', 'Mobile Payment', 'Completed', N'Số 505 Đường Khách Hàng, Quận Phú Nhuận, TP. Hồ Chí Minh', NULL, 2),
    (4, GETDATE(), 97500, 7800, 20000, 125300, 'Pending', 'Credit Card', 'Pending', N'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh', N'Ít đường', NULL);

-- OrderItem data (using the same logic, but with updated prices)
-- First order (2 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (1, 1, 1, N'Ít đá', 45000),  -- Small Classic Milk Tea
    (1, 13, 1, NULL, 57500);    -- Small Oreo Milk Tea

-- Second order (3 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (2, 5, 2, N'50% đường', 52500),  -- Medium Brown Sugar Milk Tea
    (2, 11, 1, NULL, 52500);        -- Medium Mango Slush

-- Third order (4 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (3, 9, 2, N'Không đá', 60000),    -- Large Thai Tea
    (3, 17, 1, NULL, 60000),         -- Large Honey Lemon Tea
    (3, 27, 1, NULL, 70000);         -- Large Vietnamese Coffee

-- Fourth order (2 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (4, 2, 1, N'30% đường', 52500),  -- Medium Classic Milk Tea
    (4, 18, 1, NULL, 62500);        -- Medium Matcha Latte

-- Fifth order (3 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (5, 3, 1, N'Thêm đá', 60000),    -- Large Classic Milk Tea
    (5, 6, 1, NULL, 52500),         -- Medium Taro Milk Tea
    (5, 26, 1, N'Nóng', 60000);      -- Medium Vietnamese Coffee

-- Sixth order (2 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (6, 4, 1, N'25% đường', 45000),  -- Small Taro Milk Tea 
    (6, 10, 1, NULL, 52500);        -- Small Thai Tea

-- OrderItemTopping remains the same logic
-- First order toppings
INSERT INTO [OrderItemTopping] ([order_item_id], [topping_id], [price])
VALUES 
    (1, 1, 7500),  -- Boba pearls to Classic Milk Tea
    (1, 6, 5000),  -- Fresh milk to Classic Milk Tea
    (2, 7, 10000); -- Cream cheese foam to Oreo Milk Tea

-- Continue with other OrderItemTopping inserts following the same pattern...

-- Transaction table (using VND currency)
INSERT INTO [Transaction] ([account_id], [amount], [transaction_type], [description], [order_id], [transaction_date], [processed_by])
VALUES 
    (4, 130700, 'Payment', N'Thanh toán đơn hàng #1', 1, DATEADD(DAY, -7, GETDATE()), 2),
    (5, 190100, 'Payment', N'Thanh toán đơn hàng #2', 2, DATEADD(DAY, -5, GETDATE()), 2),
    (6, 246800, 'Payment', N'Thanh toán đơn hàng #3', 3, DATEADD(DAY, -3, GETDATE()), 3),
    (7, 144200, 'Payment', N'Thanh toán đơn hàng #4', 4, DATEADD(DAY, -1, GETDATE()), 3),
    (8, 206300, 'Payment', N'Thanh toán đơn hàng #5', 5, GETDATE(), 2),
    (4, 125300, 'Payment', N'Thanh toán đơn hàng #6', 6, GETDATE(), 1);

-- Seed data for Combo table
INSERT INTO [Combo] ([combo_name], [description], [total_price], [image_path], [is_available])
VALUES 
    (N'Combo Trà Sữa Mát Lạnh', N'Hai ly trà sữa và một đá xay', 120000, 'https://static.hotdeal.vn/images/1586/1585817/60x60/355542-.jpg', 1),
    (N'Combo Gia Đình', N'Bốn ly trà khác nhau cho cả nhà', 180000, 'https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQCiWXmXB149ps9_xzKbGFgReYV5h8dpusNgg&s', 1),
    (N'Combo Sinh Viên', N'Hai ly trà và một snack', 100000, 'https://bizweb.dktcdn.net/thumb/grande/100/421/036/collections/combo-tra-sua-tien-loi.jpg?v=1620448606117', 1);

-- Seed data for ComboItem table
INSERT INTO [ComboItem] ([combo_id], [product_size_id], [quantity])
VALUES 
    (1, 1, 1),   -- Small Classic Milk Tea
    (1, 9, 1),   -- Large Mango Slush
    (2, 2, 2),   -- Medium Classic Milk Tea
    (2, 5, 1),   -- Medium Brown Sugar Milk Tea
    (2, 10, 1),  -- Small Thai Tea
    (3, 6, 1),   -- Medium Taro Milk Tea
    (3, 11, 1);  -- Medium Mango Slush

-- Seed data for OrderCombo table
INSERT INTO [OrderCombo] ([order_id], [combo_id], [quantity], [unit_price])
VALUES 
    (1, 1, 1, 120000),
    (3, 2, 1, 180000),
    (5, 3, 1, 100000);

PRINT 'Seed data has been successfully inserted into the MilkTeaShop database.';