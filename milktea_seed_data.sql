USE [MilkTeaShop];
GO

-- Seed data for Account table (with Vietnamese names and phone formats)
INSERT INTO [Account] ([email], [password], [first_name], [last_name], [phone], [address], [role])
VALUES 
    ('admin@trademilktea.vn', '123', N'Nguyễn', N'Quản Trị', '0912345678', N'Số 123 Đường Quản Trị, Quận 1, TP. Hồ Chí Minh', 'ADMIN'),
    ('staff1@trademilktea.vn', '123', N'Trần', N'Văn Nhân', '0923456789', N'Số 456 Đường Nhân Viên, Quận Tân Bình, TP. Hồ Chí Minh', 'STAFF'),
    ('staff2@trademilktea.vn', '123', N'Lê', N'Thị Nhân Viên', '0934567890', N'Số 789 Đường Nhân Viên, Quận Gò Vấp, TP. Hồ Chí Minh', 'STAFF'),
    ('customer1@example.vn', '123', N'Phạm', N'Văn Khách', '0945678901', N'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer2@example.vn', '123', N'Võ', N'Thị Khách', '0956789012', N'Số 202 Đường Khách Hàng, Quận Bình Thạnh, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer3@example.vn', '123', N'Hoàng', N'Văn Mua', '0967890123', N'Số 303 Đường Khách Hàng, Quận 5, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer4@example.vn', '123', N'Đỗ', N'Minh Khách', '0978901234', N'Số 404 Đường Khách Hàng, Quận 10, TP. Hồ Chí Minh', 'CUSTOMER'),
    ('customer5@example.vn', '123', N'Ngô', N'Thị Mua Sắm', '0989012345', N'Số 505 Đường Khách Hàng, Quận Phú Nhuận, TP. Hồ Chí Minh', 'CUSTOMER');

-- Seed data for Category table
INSERT INTO [Category] ([category_name], [description])
VALUES 
    (N'Trà Trái Cây', N'Trà tươi mát được chiết xuất từ các loại trái cây tự nhiên'),
    (N'Trà Sữa', N'Trà truyền thống kết hợp với sữa và các topping'),
    (N'Đồ Uống Đặc Biệt', N'Các loại đồ uống đặc chế và mùa vụ'),
    (N'Cà Phê', N'Các loại cà phê cao cấp với nhiều tuỳ chọn'),
    (N'Nước Đá Xay', N'Đồ uống đá xay mát lạnh cho những ngày nóng');

-- Seed data for MilkTeaProduct table
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

-- Seed data for Size table
INSERT INTO [Size] ([size_name], [price_modifier])
VALUES 
    (N'Nhỏ', 0.00),
    (N'Vừa', 7500),
    (N'Lớn', 15000);

-- Seed data for ProductSize table
INSERT INTO [ProductSize] ([product_id], [size_id], [price])
SELECT 
    p.[product_id], 
    s.[size_id], 
    p.[base_price] + s.[price_modifier] AS [price]
FROM 
    [MilkTeaProduct] p
CROSS JOIN 
    [Size] s;

-- Seed data for Topping table
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

-- Seed data for Order table
INSERT INTO [Order] ([account_id], [order_date], [subtotal], [tax], [delivery_fee], [total_amount], [status], [payment_method], [payment_status], [delivery_address], [notes], [processed_by])
VALUES 
    (4, DATEADD(DAY, -7, GETDATE()), 102500, 8200, 20000, 130700, 'Completed', 'Credit Card', 'Completed', N'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh', NULL, 2),
    (5, DATEADD(DAY, -5, GETDATE()), 157500, 12600, 20000, 190100, 'Completed', 'Debit Card', 'Completed', N'Số 202 Đường Khách Hàng, Quận Bình Thạnh, TP. Hồ Chí Minh', N'Ít đá', 2),
    (6, DATEADD(DAY, -3, GETDATE()), 210000, 16800, 20000, 246800, 'Delivered', 'Mobile Payment', 'Completed', N'Số 303 Đường Khách Hàng, Quận 5, TP. Hồ Chí Minh', NULL, 3),
    (7, DATEADD(DAY, -1, GETDATE()), 115000, 9200, 20000, 144200, 'Delivered', 'Credit Card', 'Completed', N'Số 404 Đường Khách Hàng, Quận 10, TP. Hồ Chí Minh', N'Gọi chuông cửa', 3),
    (8, GETDATE(), 172500, 13800, 20000, 206300, 'Processing', 'Mobile Payment', 'Completed', N'Số 505 Đường Khách Hàng, Quận Phú Nhuận, TP. Hồ Chí Minh', NULL, 2),
    (4, GETDATE(), 97500, 7800, 20000, 125300, 'Pending', 'Credit Card', 'Pending', N'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh', N'Ít đường', NULL);

-- Seed data for OrderItem table
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (1, 1, 1, N'Ít đá', 45000),  -- Small Classic Milk Tea
    (1, 13, 1, NULL, 57500);    -- Small Oreo Milk Tea

-- Seed data for OrderItemTopping table
INSERT INTO [OrderItemTopping] ([order_item_id], [topping_id], [price])
VALUES 
    (1, 1, 7500),  -- Boba pearls to Classic Milk Tea
    (1, 6, 5000),  -- Fresh milk to Classic Milk Tea
    (2, 7, 10000); -- Cream cheese foam to Oreo Milk Tea

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
