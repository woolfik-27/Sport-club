CREATE DATABASE IF NOT EXISTS sport_club;
USE sport_club;

CREATE TABLE users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    login VARCHAR(50) NOT NULL UNIQUE,
    password_hash VARCHAR(255) NOT NULL,
    full_name VARCHAR(100) NOT NULL,
    role ENUM('client','trainer') NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP
) ENGINE=InnoDB;

CREATE TABLE sports (
    id INT AUTO_INCREMENT PRIMARY KEY,
    name VARCHAR(50) NOT NULL UNIQUE
) ENGINE=InnoDB;

CREATE TABLE sessions (
    id INT AUTO_INCREMENT PRIMARY KEY,

    trainer_id INT NOT NULL,
    client_id INT NOT NULL,
    sport_id INT NOT NULL,

    session_date DATE NOT NULL,
    start_time TIME NOT NULL,
    end_time TIME NOT NULL,

    status ENUM('planned','completed','canceled') 
        NOT NULL DEFAULT 'planned',

    created_by INT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    CONSTRAINT chk_time CHECK (start_time < end_time),

    FOREIGN KEY (trainer_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY (client_id) REFERENCES users(id) ON DELETE CASCADE,
    FOREIGN KEY (sport_id) REFERENCES sports(id) ON DELETE RESTRICT,
    FOREIGN KEY (created_by) REFERENCES users(id) ON DELETE RESTRICT
) ENGINE=InnoDB;


CREATE UNIQUE INDEX ux_trainer_datetime
ON sessions (trainer_id, session_date, start_time);

CREATE INDEX ix_sessions_client ON sessions (client_id);
CREATE INDEX ix_sessions_date ON sessions (session_date);

CREATE TABLE achievements (
    id INT AUTO_INCREMENT PRIMARY KEY,

    title VARCHAR(100) NOT NULL,
    description TEXT,
    achievement_date DATE NOT NULL,

    sport_id INT NULL,

    created_by INT NOT NULL,
    created_at DATETIME NOT NULL DEFAULT CURRENT_TIMESTAMP,

    FOREIGN KEY (sport_id) REFERENCES sports(id) ON DELETE SET NULL,
    FOREIGN KEY (created_by) REFERENCES users(id) ON DELETE RESTRICT
) ENGINE=InnoDB;


CREATE INDEX ix_achievements_date 
ON achievements (achievement_date);


INSERT INTO users (login, password_hash, full_name, role)
VALUES
('client1', '123', 'Иван Иванов', 'client'),
('client2', '123', 'Алексей Смирнов', 'client'),
('trainer1', '123', 'Петр Петров', 'trainer');

INSERT INTO sports (name)
VALUES
('Фитнес'),
('Бокс'),
('Йога');

INSERT INTO sessions
(trainer_id, client_id, sport_id, session_date, start_time, end_time, status, created_by)
VALUES
(3, 1, 1, '2026-03-10', '10:00', '11:00', 'planned', 3),
(3, 2, 2, '2026-03-11', '12:00', '13:00', 'completed', 3),
(3, 1, 3, '2026-03-12', '14:00', '15:00', 'canceled', 3);

INSERT INTO achievements
(title, description, achievement_date, sport_id, created_by)
VALUES
('Лучший спортивный клуб года',
 'Победа в городском конкурсе среди фитнес-клубов',
 '2025-12-20', NULL, 3),

('Победа в турнире по боксу',
 'Первое место в региональном соревновании',
 '2025-11-10', 2, 3),

('Организация фестиваля йоги',
 'Проведение городского мероприятия',
 '2025-10-05', 3, 3);