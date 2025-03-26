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
