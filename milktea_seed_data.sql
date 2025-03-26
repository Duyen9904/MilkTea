USE [MilkTeaShop];
GO

-- Seed data for Account table (with Vietnamese names and phone formats)
INSERT INTO [Account] ([email], [password], [first_name], [last_name], [phone], [address], [role])
VALUES 
    ('admin@trademilktea.vn', 'hashed_password_123', 'Nguyễn', 'Quản Trị', '0912345678', 'Số 123 Đường Quản Trị, Quận 1, TP. Hồ Chí Minh', 'ADMIN'),
    ('staff1@trademilktea.vn', 'hashed_password_456', 'Trần', 'Văn Nhân', '0923456789', 'Số 456 Đường Nhân Viên, Quận Tân Bình, TP. Hồ Chí Minh', 'STAFF'),
    ('staff2@trademilktea.vn', 'hashed_password_789', 'Lê', 'Thị Nhân Viên', '0934567890', 'Số 789 Đường Nhân Viên, Quận Gò Vấp, TP. Hồ Chí Minh', 'STAFF'),
    ('customer1@example.vn', 'hashed_password_abc', 'Phạm', 'Văn Khách', '0945678901', 'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer2@example.vn', 'hashed_password_def', 'Võ', 'Thị Khách', '0956789012', 'Số 202 Đường Khách Hàng, Quận Bình Thạnh, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer3@example.vn', 'hashed_password_ghi', 'Hoàng', 'Văn Mua', '0967890123', 'Số 303 Đường Khách Hàng, Quận 5, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer4@example.vn', 'hashed_password_jkl', 'Đỗ', 'Minh Khách', '0978901234', 'Số 404 Đường Khách Hàng, Quận 10, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer5@example.vn', 'hashed_password_mno', 'Ngô', 'Thị Mua Sắm', '0989012345', 'Số 505 Đường Khách Hàng, Quận Phú Nhuận, TP. Hồ Chí Minh', 'CUSTOMER');

-- Keep Category seed data the same
INSERT INTO [Category] ([category_name], [description])
VALUES 
    ('Trà Trái Cây', 'Trà tươi mát được chiết xuất từ các loại trái cây tự nhiên'),
    ('Trà Sữa', 'Trà truyền thống kết hợp với sữa và các topping'),
    ('Đồ Uống Đặc Biệt', 'Các loại đồ uống đặc chế và mùa vụ'),
    ('Cà Phê', 'Các loại cà phê cao cấp với nhiều tuỳ chọn'),
    ('Nước Đá Xay', 'Đồ uống đá xay mát lạnh cho những ngày nóng');

-- Update MilkTeaProduct with Vietnamese names and descriptions
INSERT INTO [MilkTeaProduct] ([product_name], [category_id], [description], [base_price], [image_path], [is_available])
VALUES 
    ('Trà Sữa Truyền Thống', 2, 'Trà sữa đen kinh điển', 45000, 'classic_milk_tea.jpg', 1),
    ('Trà Sữa Khoai Môn', 2, 'Trà sữa khoai môn béo ngậy', 47500, 'taro_milk_tea.jpg', 1),
    ('Trà Sữa Đường Nâu', 2, 'Trà sữa với siro đường nâu và trân châu', 52500, 'brown_sugar_milk_tea.jpg', 1),
    ('Trà Xanh Xoài', 1, 'Trà xanh thơm mát với vị xoài', 45000, 'mango_green_tea.jpg', 1),
    ('Trà Vải', 1, 'Trà đen với hương vị trái vải', 45000, 'lychee_fruit_tea.jpg', 1),
    ('Trà Dâu', 1, 'Trà tươi mát với hương dâu', 45000, 'strawberry_fruit_tea.jpg', 1),
    ('Matcha Sữa', 3, 'Matcha cao cấp kết hợp sữa', 55000, 'matcha_latte.jpg', 1),
    ('Cà Phê Việt Nam', 4, 'Cà phê đậm đà với sữa đặc', 47500, 'vietnamese_coffee.jpg', 1),
    ('Xoài Đá Xay', 5, 'Đá xay mát lạnh vị xoài', 50000, 'mango_slush.jpg', 1),
    ('Trà Thái', 2, 'Trà Thái truyền thống', 47500, 'thai_tea.jpg', 1),
    ('Trà Chanh Mật Ong', 1, 'Trà đen với mật ong và chanh tươi', 42500, 'honey_lemon_tea.jpg', 1),
    ('Trà Sữa Oreo', 3, 'Trà sữa kết hợp bánh quy Oreo', 57500, 'oreo_milk_tea.jpg', 1);

-- Size table remains the same
INSERT INTO [Size] ([size_name], [price_modifier])
VALUES 
    ('Nhỏ', 0.00),
    ('Vừa', 7500),
    ('Lớn', 15000);

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
    ('Trân Châu', 'Trân châu truyền thống', 7500, 'boba_pearls.jpg', 1),
    ('Thạch Cỏ Biển', 'Thạch ngọt từ rong biển', 7500, 'grass_jelly.jpg', 1),
    ('Nha Đam', 'Viên nha đam ngọt ngào', 7500, 'aloe_vera.jpg', 1),
    ('Pudding', 'Pudding trứng mịn', 7500, 'pudding.jpg', 1),
    ('Trân Châu Trong', 'Trân châu trong suốt', 7500, 'crystal_boba.jpg', 1),
    ('Sữa Tươi', 'Sữa tươi nguyên chất', 5000, 'fresh_milk.jpg', 1),
    ('Kem Cheese', 'Lớp kem cheese mặn ngọt', 10000, 'cream_cheese.jpg', 1),
    ('Thạch Cà Phê', 'Thạch vị cà phê', 7500, 'coffee_jelly.jpg', 1),
    ('Thạch Vải', 'Thạch vị trái vải', 7500, 'lychee_jelly.jpg', 1),
    ('Trái Cây Tươi', 'Các miếng trái cây tươi', 12500, 'fresh_fruit.jpg', 1);

-- Updated Order data with Vietnamese-style pricing and details
INSERT INTO [Order] ([account_id], [order_date], [subtotal], [tax], [delivery_fee], [total_amount], [status], [payment_method], [payment_status], [delivery_address], [notes], [processed_by])
VALUES 
    (4, DATEADD(DAY, -7, GETDATE()), 102500, 8200, 20000, 130700, 'Completed', 'Credit Card', 'Completed', 'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh', NULL, 2),
    (5, DATEADD(DAY, -5, GETDATE()), 157500, 12600, 20000, 190100, 'Completed', 'Debit Card', 'Completed', 'Số 202 Đường Khách Hàng, Quận Bình Thạnh, TP. Hồ Chí Minh', 'Ít đá', 2),
    (6, DATEADD(DAY, -3, GETDATE()), 210000, 16800, 20000, 246800, 'Delivered', 'Mobile Payment', 'Completed', 'Số 303 Đường Khách Hàng, Quận 5, TP. Hồ Chí Minh', NULL, 3),
    (7, DATEADD(DAY, -1, GETDATE()), 115000, 9200, 20000, 144200, 'Delivered', 'Credit Card', 'Completed', 'Số 404 Đường Khách Hàng, Quận 10, TP. Hồ Chí Minh', 'Gọi chuông cửa', 3),
    (8, GETDATE(), 172500, 13800, 20000, 206300, 'Processing', 'Mobile Payment', 'Completed', 'Số 505 Đường Khách Hàng, Quận Phú Nhuận, TP. Hồ Chí Minh', NULL, 2),
    (4, GETDATE(), 97500, 7800, 20000, 125300, 'Pending', 'Credit Card', 'Pending', 'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh', 'Ít đường', NULL);

-- OrderItem data (using the same logic, but with updated prices)
-- First order (2 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (1, 1, 1, 'Ít đá', 45000),  -- Small Classic Milk Tea
    (1, 13, 1, NULL, 57500);    -- Small Oreo Milk Tea

-- Second order (3 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (2, 5, 2, '50% đường', 52500),  -- Medium Brown Sugar Milk Tea
    (2, 11, 1, NULL, 52500);        -- Medium Mango Slush

-- Third order (4 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (3, 9, 2, 'Không đá', 60000),    -- Large Thai Tea
    (3, 17, 1, NULL, 60000),         -- Large Honey Lemon Tea
    (3, 27, 1, NULL, 70000);         -- Large Vietnamese Coffee

-- Fourth order (2 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (4, 2, 1, '30% đường', 52500),  -- Medium Classic Milk Tea
    (4, 18, 1, NULL, 62500);        -- Medium Matcha Latte

-- Fifth order (3 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (5, 3, 1, 'Thêm đá', 60000),    -- Large Classic Milk Tea
    (5, 6, 1, NULL, 52500),         -- Medium Taro Milk Tea
    (5, 26, 1, 'Nóng', 60000);      -- Medium Vietnamese Coffee

-- Sixth order (2 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (6, 4, 1, '25% đường', 45000),  -- Small Taro Milk Tea 
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
    (4, 130700, 'Payment', 'Thanh toán đơn hàng #1', 1, DATEADD(DAY, -7, GETDATE()), 2),
    (5, 190100, 'Payment', 'Thanh toán đơn hàng #2', 2, DATEADD(DAY, -5, GETDATE()), 2),
    (6, 246800, 'Payment', 'Thanh toán đơn hàng #3', 3, DATEADD(DAY, -3, GETDATE()), 3),
    (7, 144200, 'Payment', 'Thanh toán đơn hàng #4', 4, DATEADD(DAY, -1, GETDATE()), 3),
    (8, 206300, 'Payment', 'Thanh toán đơn hàng #5', 5, GETDATE(), 2),
    (4, 125300, 'Payment', 'Thanh toán đơn hàng #6', 6, GETDATE(), 1);

-- Seed data for Combo table
INSERT INTO [Combo] ([combo_name], [description], [total_price], [image_path], [is_available])
VALUES 
    ('Combo Trà Sữa Mát Lạnh', 'Hai ly trà sữa và một đá xay', 120000, 'cool_milk_tea_combo.jpg', 1),
    ('Combo Gia Đình', 'Bốn ly trà khác nhau cho cả nhà', 180000, 'family_tea_combo.jpg', 1),
    ('Combo Sinh Viên', 'Hai ly trà và một snack', 100000, 'student_combo.jpg', 1);

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