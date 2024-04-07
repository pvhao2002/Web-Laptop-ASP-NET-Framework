CREATE DATABASE shop_laptop;
GO
USE shop_laptop;
GO
CREATE TABLE users (
    user_id INT IDENTITY PRIMARY KEY,
    email NVARCHAR(255) ,
    password NVARCHAR(255) ,
    full_name NVARCHAR(255) ,
    role VARCHAR(50)  DEFAULT 'user',
    status VARCHAR(50) DEFAULT 'active',
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);
GO
CREATE TABLE categories (
    category_id INT IDENTITY PRIMARY KEY,
    category_name NVARCHAR(255) ,
    description TEXT,
    status VARCHAR(50)  DEFAULT 'active',
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE()
);
GO
CREATE TABLE products (
    product_id INT IDENTITY PRIMARY KEY,
    product_name NVARCHAR(255) ,
    price DECIMAL(10, 2) ,
    description TEXT,
    product_image NVARCHAR(2000),
    category_id INT,
    status VARCHAR(50)  DEFAULT 'active',
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (category_id) REFERENCES categories(category_id)
);
GO
CREATE TABLE carts (
    cart_id INT IDENTITY PRIMARY KEY,
    user_id INT ,
    total_price DECIMAL(10, 2) ,
    total_quantity INT ,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);
GO
CREATE TABLE cart_items (
    cart_item_id INT IDENTITY PRIMARY KEY,
    cart_id INT ,
    product_id INT ,
    quantity INT ,
    total_price DECIMAL(10, 2) ,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (cart_id) REFERENCES carts(cart_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);
GO
CREATE TABLE orders (
    order_id INT IDENTITY PRIMARY KEY,
    user_id INT ,
    shipping_address NVARCHAR(255) ,
    phone_number NVARCHAR(20) ,
    full_name NVARCHAR(255) ,
    total_price DECIMAL(10, 2) ,
    total_quantity INT ,
    status VARCHAR(50) DEFAULT 'pending',
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (user_id) REFERENCES users(user_id)
);
GO
CREATE TABLE order_items (
    order_item_id INT IDENTITY PRIMARY KEY,
    order_id INT ,
    product_id INT ,
    quantity INT ,
    total_price DECIMAL(10, 2) ,
    created_at DATETIME DEFAULT GETDATE(),
    updated_at DATETIME DEFAULT GETDATE(),
    FOREIGN KEY (order_id) REFERENCES orders(order_id),
    FOREIGN KEY (product_id) REFERENCES products(product_id)
);
-- INSERT DATA FOR TABLE users
INSERT INTO users (email, password, full_name, role, status)
VALUES (
        'admin@gmail.com',
        '1234qwer',
        'admin',
        'admin',
        'active'
    );
INSERT INTO users (
        email,
        password,
        full_name,
        role,
        status
    )
VALUES (
        'user1@example.com',
        '1234qwer',
        'User 1',
        'user',
        'active'
    ),
    (
        'user2@example.com',
        '1234qwer',
        'User 2',
        'user',
        'active'
    ),
    (
        'user3@example.com',
        '1234qwer',
        'User 3',
        'user',
        'active'
    ),
    (
        'user4@example.com',
        '1234qwer',
        'User 4',
        'user',
        'active'
    ),
    (
        'user5@example.com',
        '1234qwer',
        'User 5',
        'user',
        'active'
    ),
    (
        'user6@example.com',
        '1234qwer',
        'User 6',
        'user',
        'active'
    ),
    (
        'user7@example.com',
        '1234qwer',
        'User 7',
        'user',
        'active'
    ),
    (
        'user8@example.com',
        '1234qwer',
        'User 8',
        'user',
        'active'
    ),
    (
        'user9@example.com',
        '1234qwer',
        'User 9',
        'user',
        'active'
    );
-- INSERT DATA FOR TABLE categories
INSERT INTO categories (category_name, description, status)
VALUES (
        'Acer',
        'Laptop acer',
        'active'
    ),
    (
        'Asus',
        'Laptop asus',
        'active'
    ),
    (
        'Dell',
        'Laptop dell',
        'active'
    ),
    (
        'HP',
        'Laptop hp',
        'active'
    ),
    (
        'Lenovo',
        'Laptop lenovo',
        'active'
    );