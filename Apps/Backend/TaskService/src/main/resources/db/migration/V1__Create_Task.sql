-- Tạo bảng Task trong PostgreSQL
CREATE TABLE task (
    task_id SERIAL PRIMARY KEY,      -- AUTO_INCREMENT trong MySQL -> SERIAL trong PostgreSQL
    name VARCHAR(255) NOT NULL,
    user_id INT NOT NULL,
    expiration DATE NOT NULL
);

-- Insert dữ liệu mẫu
INSERT INTO task (task_id, name, user_id, expiration) VALUES
(1, 'Lau nhà', 1, '2025-09-15'),
(2, 'thanhphat9523@gmail.com', 1, '2025-09-15'),
(3, 'thanhphat9523@gmail.com', 1, '2025-09-15'),
(4, 'Quét sân', 1, '2025-09-28'),
(5, 'Rửa chén', 1, '2025-09-28'),
(6, 'hẹ hẹ', 1, '2025-09-28'),
(7, 'hẹ hẹ hẹ', 1, '2025-09-28');

-- Reset sequence để SERIAL tiếp tục chạy từ 8 trở đi
ALTER SEQUENCE task_task_id_seq RESTART WITH 8;
