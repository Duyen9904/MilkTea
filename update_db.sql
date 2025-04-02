

UPDATE [MilkTeaShop].[dbo].[Size]
SET 
  [size_name] = 
    CASE [size_id]
      WHEN 1 THEN N'Nhỏ'
      WHEN 2 THEN N'Vừa'
      WHEN 3 THEN N'Lớn'
    END,
  [price_modifier] = 
    CASE [size_id]
      WHEN 1 THEN 0.00
      WHEN 2 THEN 7500.00
      WHEN 3 THEN 15000.00
    END
WHERE [size_id] IN (1, 2, 3);


  
  UPDATE MilkTeaProduct SET 
    product_name = N'Trà Sữa Truyền Thống',
    description = N'Trà sữa đen kinh điển'
WHERE product_id = 1;

UPDATE MilkTeaProduct SET 
    product_name = N'Trà Sữa Khoai Môn',
    description = N'Trà sữa khoai môn béo ngậy'
WHERE product_id = 2;

UPDATE MilkTeaProduct SET 
    product_name = N'Trà Sữa Đường Nâu',
    description = N'Trà sữa với siro đường nâu và trân châu'
WHERE product_id = 3;

UPDATE MilkTeaProduct SET 
    product_name = N'Trà Xanh Xoài',
    description = N'Trà xanh thơm mát với vị xoài'
WHERE product_id = 4;

UPDATE MilkTeaProduct SET 
    product_name = N'Trà Vải',
    description = N'Trà đen với hương vị trái vải'
WHERE product_id = 5;

UPDATE MilkTeaProduct SET 
    product_name = N'Trà Dâu',
    description = N'Trà tươi mát với hương dâu'
WHERE product_id = 6;

UPDATE MilkTeaProduct SET 
    product_name = N'Matcha Sữa',
    description = N'Matcha cao cấp kết hợp sữa'
WHERE product_id = 7;

UPDATE MilkTeaProduct SET 
    product_name = N'Cà Phê Việt Nam',
    description = N'Cà phê đậm đà với sữa đặc'
WHERE product_id = 8;

UPDATE MilkTeaProduct SET 
    product_name = N'Xoài Đá Xay',
    description = N'Đá xay mát lạnh vị xoài'
WHERE product_id = 9;

UPDATE MilkTeaProduct SET 
    product_name = N'Trà Thái',
    description = N'Trà Thái truyền thống'
WHERE product_id = 10;

UPDATE MilkTeaProduct SET 
    product_name = N'Trà Chanh Mật Ong',
    description = N'Trà đen với mật ong và chanh tươi'
WHERE product_id = 11;

UPDATE MilkTeaProduct SET 
    product_name = N'Trà Sữa Oreo',
    description = N'Trà sữa kết hợp bánh quy Oreo'
WHERE product_id = 12;




  UPDATE [MilkTeaShop].[dbo].[Category]
SET 
  [category_name] = 
    CASE [category_id]
      WHEN 1 THEN N'Trà Trái Cây'
      WHEN 2 THEN N'Trà Sữa'
      WHEN 3 THEN N'Đồ Uống Đặc Biệt'
      WHEN 4 THEN N'Cà Phê'
      WHEN 5 THEN N'Nước Đá Xay'
    END,
  [description] = 
    CASE [category_id]
      WHEN 1 THEN N'Trà tươi mát được chiết xuất từ các loại trái cây tự nhiên'
      WHEN 2 THEN N'Trà truyền thống kết hợp với sữa và các topping'
      WHEN 3 THEN N'Các loại đồ uống đặc chế và mùa vụ'
      WHEN 4 THEN N'Các loại cà phê cao cấp với nhiều tuỳ chọn'
      WHEN 5 THEN N'Đồ uống đá xay mát lạnh cho những ngày nóng'
    END
WHERE [category_id] IN (1, 2, 3, 4, 5);



UPDATE [MilkTeaShop].[dbo].[MilkTeaProduct]
SET [image_path] = N'https://file.hstatic.net/1000135323/article/tra_sua_ngon_1ebddd76eed14d708abc4a9ee597b304.jpg';


UPDATE [MilkTeaShop].[dbo].[Account]
SET 
  [first_name] = 
    CASE [account_id]
      WHEN 1 THEN N'Nguyễn'
      WHEN 2 THEN N'Trần'
      WHEN 3 THEN N'Lê'
      WHEN 4 THEN N'Phạm'
      WHEN 5 THEN N'Võ'
      WHEN 6 THEN N'Hoàng'
      WHEN 7 THEN N'Đỗ'
      WHEN 8 THEN N'Ngô'
    END,
  [last_name] = 
    CASE [account_id]
      WHEN 1 THEN N'Quản Trị'
      WHEN 2 THEN N'Văn Nhân'
      WHEN 3 THEN N'Thị Nhân Viên'
      WHEN 4 THEN N'Văn Khách'
      WHEN 5 THEN N'Thị Khách'
      WHEN 6 THEN N'Văn Mua'
      WHEN 7 THEN N'Minh Khách'
      WHEN 8 THEN N'Thị Mua Sắm'
    END,
  [address] =
    CASE [account_id]
      WHEN 1 THEN N'Số 123 Đường Quản Trị, Quận 1, TP. Hồ Chí Minh'
      WHEN 2 THEN N'Số 456 Đường Nhân Viên, Quận Tân Bình, TP. Hồ Chí Minh'
      WHEN 3 THEN N'Số 789 Đường Nhân Viên, Quận Gò Vấp, TP. Hồ Chí Minh'
      WHEN 4 THEN N'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh'
      WHEN 5 THEN N'Số 202 Đường Khách Hàng, Quận Bình Thạnh, TP. Hồ Chí Minh'
      WHEN 6 THEN N'Số 303 Đường Khách Hàng, Quận 5, TP. Hồ Chí Minh'
      WHEN 7 THEN N'Số 404 Đường Khách Hàng, Quận 10, TP. Hồ Chí Minh'
      WHEN 8 THEN N'Số 505 Đường Khách Hàng, Quận Phú Nhuận, TP. Hồ Chí Minh'
    END
WHERE [account_id] IN (1,2,3,4,5,6,7,8);


UPDATE [MilkTeaShop].[dbo].[Topping]
SET 
  [topping_name] = 
    CASE [topping_id]
      WHEN 1 THEN N'Trân Châu'
      WHEN 2 THEN N'Thạch Cỏ Biển'
      WHEN 3 THEN N'Nha Đam'
      WHEN 4 THEN N'Pudding'
      WHEN 5 THEN N'Trân Châu Trong'
      WHEN 6 THEN N'Sữa Tươi'
      WHEN 7 THEN N'Kem Cheese'
      WHEN 8 THEN N'Thạch Cà Phê'
      WHEN 9 THEN N'Thạch Vải'
      WHEN 10 THEN N'Trái Cây Tươi'
    END,
  [description] = 
    CASE [topping_id]
      WHEN 1 THEN N'Trân châu truyền thống'
      WHEN 2 THEN N'Thạch ngọt từ rong biển'
      WHEN 3 THEN N'Viên nha đam ngọt ngào'
      WHEN 4 THEN N'Pudding trứng mềm'
      WHEN 5 THEN N'Trân châu trong suốt'
      WHEN 6 THEN N'Sữa tươi nguyên chất'
      WHEN 7 THEN N'Lớp kem cheese mặn ngọt'
      WHEN 8 THEN N'Thạch vị cà phê'
      WHEN 9 THEN N'Thạch vị trái vải'
      WHEN 10 THEN N'Các miếng trái cây tươi'
    END
WHERE [topping_id] IN (1,2,3,4,5,6,7,8,9,10);




UPDATE [MilkTeaShop].[dbo].[Combo]
SET 
  [combo_name] = 
    CASE [combo_id]
      WHEN 1 THEN N'Combo Trà Sữa Mát Lạnh'
      WHEN 2 THEN N'Combo Gia Đình'
      WHEN 3 THEN N'Combo Sinh Viên'
    END,
  [description] = 
    CASE [combo_id]
      WHEN 1 THEN N'Hai ly trà sữa và một đá xay'
      WHEN 2 THEN N'Bốn ly trà khác nhau cho cả nhà'
      WHEN 3 THEN N'Hai ly trà và một snack'
    END
WHERE [combo_id] IN (1, 2, 3);



UPDATE [MilkTeaShop].[dbo].[Order]
SET 
  [delivery_address] = 
    CASE [order_id]
      WHEN 1 THEN N'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh'
      WHEN 2 THEN N'Số 202 Đường Khách Hàng, Quận Bình Thạnh, TP. Hồ Chí Minh'
      WHEN 3 THEN N'Số 303 Đường Khách Hàng, Quận 5, TP. Hồ Chí Minh'
      WHEN 4 THEN N'Số 404 Đường Khách Hàng, Quận 10, TP. Hồ Chí Minh'
      WHEN 5 THEN N'Số 505 Đường Khách Hàng, Quận Phú Nhuận, TP. Hồ Chí Minh'
      WHEN 6 THEN N'Số 101 Đường Khách Hàng, Quận 3, TP. Hồ Chí Minh'
    END,
  [notes] =
    CASE [order_id]
      WHEN 2 THEN N'Ít đá'
      WHEN 4 THEN N'Gọi chuông cửa'
      WHEN 6 THEN N'Ít đường'
      ELSE NULL
    END
WHERE [order_id] IN (1,2,3,4,5,6);
