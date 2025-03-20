USE [MilkTeaShop];
GO

-- Seed data for Account table
INSERT INTO [Account] ([email], [password], [first_name], [last_name], [phone], [address], [role])
VALUES 
    ('admin@milkteashop.com', 'hashed_password_123', 'Admin', 'User', '555-123-4567', '123 Admin St, City, ST 12345', 'ADMIN'),
    ('staff1@milkteashop.com', 'hashed_password_456', 'John', 'Smith', '555-234-5678', '456 Staff Ave, City, ST 12345', 'STAFF'),
    ('staff2@milkteashop.com', 'hashed_password_789', 'Jane', 'Doe', '555-345-6789', '789 Staff Blvd, City, ST 12345', 'STAFF'),
    ('customer1@example.com', 'hashed_password_abc', 'Alice', 'Johnson', '555-456-7890', '101 Customer Ln, City, ST 12345', 'CUSTOMER'),
    ('customer2@example.com', 'hashed_password_def', 'Bob', 'Williams', '555-567-8901', '202 Customer Rd, City, ST 12345', 'CUSTOMER'),
    ('customer3@example.com', 'hashed_password_ghi', 'Carol', 'Brown', '555-678-9012', '303 Customer Pl, City, ST 12345', 'CUSTOMER'),
    ('customer4@example.com', 'hashed_password_jkl', 'David', 'Miller', '555-789-0123', '404 Customer Ct, City, ST 12345', 'CUSTOMER'),
    ('customer5@example.com', 'hashed_password_mno', 'Eva', 'Davis', '555-890-1234', '505 Customer Way, City, ST 12345', 'CUSTOMER');

-- Seed data for Category table
INSERT INTO [Category] ([category_name], [description])
VALUES 
    ('Fruit Tea', 'Refreshing tea infused with natural fruit flavors'),
    ('Milk Tea', 'Traditional tea mixed with milk and various toppings'),
    ('Special Drinks', 'Signature house specialties and seasonal offerings'),
    ('Coffee', 'Premium coffee drinks with various customizations'),
    ('Slush', 'Frozen blended beverages perfect for hot days');

-- Seed data for MilkTeaProduct table
INSERT INTO [MilkTeaProduct] ([product_name], [category_id], [description], [base_price], [image_path], [is_available])
VALUES 
    ('Classic Milk Tea', 2, 'Our traditional milk tea with black tea base', 4.50, 'classic_milk_tea.jpg', 1),
    ('Taro Milk Tea', 2, 'Creamy taro-flavored milk tea', 4.75, 'taro_milk_tea.jpg', 1),
    ('Brown Sugar Milk Tea', 2, 'Milk tea with brown sugar syrup and pearls', 5.25, 'brown_sugar_milk_tea.jpg', 1),
    ('Mango Green Tea', 1, 'Green tea infused with sweet mango', 4.50, 'mango_green_tea.jpg', 1),
    ('Lychee Fruit Tea', 1, 'Black tea with lychee fruit flavor', 4.50, 'lychee_fruit_tea.jpg', 1),
    ('Strawberry Fruit Tea', 1, 'Refreshing tea with strawberry flavor', 4.50, 'strawberry_fruit_tea.jpg', 1),
    ('Matcha Latte', 3, 'Premium matcha green tea with milk', 5.50, 'matcha_latte.jpg', 1),
    ('Vietnamese Coffee', 4, 'Strong coffee with condensed milk', 4.75, 'vietnamese_coffee.jpg', 1),
    ('Mango Slush', 5, 'Frozen mango blended beverage', 5.00, 'mango_slush.jpg', 1),
    ('Thai Tea', 2, 'Traditional Thai tea with milk', 4.75, 'thai_tea.jpg', 1),
    ('Honey Lemon Tea', 1, 'Black tea with honey and fresh lemon', 4.25, 'honey_lemon_tea.jpg', 1),
    ('Oreo Milk Tea', 3, 'Milk tea blended with Oreo cookies', 5.75, 'oreo_milk_tea.jpg', 1);

-- Seed data for Size table
INSERT INTO [Size] ([size_name], [price_modifier])
VALUES 
    ('Small', 0.00),
    ('Medium', 0.75),
    ('Large', 1.50);

-- Seed data for ProductSize table (connecting products with sizes and setting prices)
-- For each product, create all available sizes
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
    ('Boba Pearls', 'Classic tapioca pearls', 0.75, 'boba_pearls.jpg', 1),
    ('Grass Jelly', 'Sweet, herbal jelly cubes', 0.75, 'grass_jelly.jpg', 1),
    ('Aloe Vera', 'Sweet aloe vera cubes', 0.75, 'aloe_vera.jpg', 1),
    ('Pudding', 'Smooth egg pudding', 0.75, 'pudding.jpg', 1),
    ('Crystal Boba', 'Translucent tapioca pearls', 0.75, 'crystal_boba.jpg', 1),
    ('Fresh Milk', 'Premium fresh milk', 0.50, 'fresh_milk.jpg', 1),
    ('Cream Cheese Foam', 'Savory-sweet cream cheese topping', 1.00, 'cream_cheese.jpg', 1),
    ('Coffee Jelly', 'Coffee-flavored jelly cubes', 0.75, 'coffee_jelly.jpg', 1),
    ('Lychee Jelly', 'Sweet lychee-flavored jelly', 0.75, 'lychee_jelly.jpg', 1),
    ('Fresh Fruit', 'Selection of fresh fruit pieces', 1.25, 'fresh_fruit.jpg', 1);

-- Seed data for Order table
INSERT INTO [Order] ([account_id], [order_date], [subtotal], [tax], [delivery_fee], [total_amount], [status], [payment_method], [payment_status], [delivery_address], [notes], [processed_by])
VALUES 
    (4, DATEADD(DAY, -7, GETDATE()), 10.25, 0.82, 2.00, 13.07, 'Completed', 'Credit Card', 'Completed', '101 Customer Ln, City, ST 12345', NULL, 2),
    (5, DATEADD(DAY, -5, GETDATE()), 15.75, 1.26, 2.00, 19.01, 'Completed', 'Debit Card', 'Completed', '202 Customer Rd, City, ST 12345', 'Extra ice please', 2),
    (6, DATEADD(DAY, -3, GETDATE()), 21.00, 1.68, 2.00, 24.68, 'Delivered', 'Mobile Payment', 'Completed', '303 Customer Pl, City, ST 12345', NULL, 3),
    (7, DATEADD(DAY, -1, GETDATE()), 11.50, 0.92, 2.00, 14.42, 'Delivered', 'Credit Card', 'Completed', '404 Customer Ct, City, ST 12345', 'Ring doorbell', 3),
    (8, GETDATE(), 17.25, 1.38, 2.00, 20.63, 'Processing', 'Mobile Payment', 'Completed', '505 Customer Way, City, ST 12345', NULL, 2),
    (4, GETDATE(), 9.75, 0.78, 2.00, 12.53, 'Pending', 'Credit Card', 'Pending', '101 Customer Ln, City, ST 12345', 'Less sugar', NULL);

-- Seed data for OrderItem table
-- First order (2 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (1, 1, 1, 'Less ice', 4.50),  -- Small Classic Milk Tea
    (1, 13, 1, NULL, 5.75);      -- Small Oreo Milk Tea

-- Second order (3 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (2, 5, 2, '50% sugar', 5.25),  -- Medium Brown Sugar Milk Tea
    (2, 11, 1, NULL, 5.25);       -- Medium Mango Slush

-- Third order (4 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (3, 9, 2, 'No ice', 6.00),     -- Large Thai Tea
    (3, 17, 1, NULL, 6.00),        -- Large Honey Lemon Tea
    (3, 27, 1, NULL, 7.00);        -- Large Vietnamese Coffee

-- Fourth order (2 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (4, 2, 1, '30% sugar', 5.25),  -- Medium Classic Milk Tea
    (4, 18, 1, NULL, 6.25);        -- Medium Matcha Latte

-- Fifth order (3 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (5, 3, 1, 'Extra ice', 6.00),  -- Large Classic Milk Tea
    (5, 6, 1, NULL, 5.25),         -- Medium Taro Milk Tea
    (5, 26, 1, 'Hot', 6.00);       -- Medium Vietnamese Coffee

-- Sixth order (2 items)
INSERT INTO [OrderItem] ([order_id], [product_size_id], [quantity], [special_instructions], [unit_price])
VALUES 
    (6, 4, 1, '25% sugar', 4.50),  -- Small Taro Milk Tea 
    (6, 10, 1, NULL, 5.25);        -- Small Thai Tea

-- Seed data for OrderItemTopping table
-- Add toppings to first order items
INSERT INTO [OrderItemTopping] ([order_item_id], [topping_id], [price])
VALUES 
    (1, 1, 0.75),  -- Boba pearls to Classic Milk Tea
    (1, 6, 0.50),  -- Fresh milk to Classic Milk Tea
    (2, 7, 1.00);  -- Cream cheese foam to Oreo Milk Tea

-- Add toppings to second order items
INSERT INTO [OrderItemTopping] ([order_item_id], [topping_id], [price])
VALUES 
    (3, 1, 0.75),  -- Boba pearls to Brown Sugar Milk Tea
    (3, 4, 0.75),  -- Pudding to Brown Sugar Milk Tea
    (4, 10, 1.25); -- Fresh fruit to Mango Slush

-- Add toppings to third order items
INSERT INTO [OrderItemTopping] ([order_item_id], [topping_id], [price])
VALUES 
    (5, 2, 0.75),  -- Grass jelly to Thai Tea
    (5, 7, 1.00),  -- Cream cheese foam to Thai Tea
    (6, 10, 1.25), -- Fresh fruit to Honey Lemon Tea
    (7, 8, 0.75);  -- Coffee jelly to Vietnamese Coffee

-- Add toppings to fourth order items
INSERT INTO [OrderItemTopping] ([order_item_id], [topping_id], [price])
VALUES 
    (8, 1, 0.75),  -- Boba pearls to Classic Milk Tea
    (9, 4, 0.75);  -- Pudding to Matcha Latte

-- Add toppings to fifth order items
INSERT INTO [OrderItemTopping] ([order_item_id], [topping_id], [price])
VALUES 
    (10, 1, 0.75), -- Boba pearls to Classic Milk Tea
    (10, 2, 0.75), -- Grass jelly to Classic Milk Tea
    (11, 4, 0.75), -- Pudding to Taro Milk Tea
    (12, 8, 0.75); -- Coffee jelly to Vietnamese Coffee

-- Add toppings to sixth order items
INSERT INTO [OrderItemTopping] ([order_item_id], [topping_id], [price])
VALUES 
    (13, 1, 0.75), -- Boba pearls to Taro Milk Tea
    (14, 7, 1.00); -- Cream cheese foam to Thai Tea

-- Seed data for Transaction table
INSERT INTO [Transaction] ([account_id], [amount], [transaction_type], [description], [order_id], [transaction_date], [processed_by])
VALUES 
    (4, 13.07, 'Payment', 'Payment for order #1', 1, DATEADD(DAY, -7, GETDATE()), 2),
    (5, 19.01, 'Payment', 'Payment for order #2', 2, DATEADD(DAY, -5, GETDATE()), 2),
    (6, 24.68, 'Payment', 'Payment for order #3', 3, DATEADD(DAY, -3, GETDATE()), 3),
    (7, 14.42, 'Payment', 'Payment for order #4', 4, DATEADD(DAY, -1, GETDATE()), 3),
    (8, 20.63, 'Payment', 'Payment for order #5', 5, GETDATE(), 2),
    (4, 12.53, 'Payment', 'Payment for order #6', 6, GETDATE(), 1);

PRINT 'Seed data has been successfully inserted into the MilkTeaShop database.';