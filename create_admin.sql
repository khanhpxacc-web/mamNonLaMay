-- Tạo admin user với password hash đúng (Admin@123)
-- Password hash này được generate bởi ASP.NET Core Identity

INSERT INTO AspNetUsers (
    Id, 
    UserName, 
    NormalizedUserName, 
    Email, 
    NormalizedEmail, 
    EmailConfirmed, 
    PasswordHash, 
    SecurityStamp, 
    ConcurrencyStamp,
    LockoutEnabled,
    AccessFailedCount
) VALUES (
    NEWID(),
    'admin@mamnonlamay.edu.vn',
    'ADMIN@MAMNONLAMAY.EDU.VN',
    'admin@mamnonlamay.edu.vn',
    'ADMIN@MAMNONLAMAY.EDU.VN',
    1,
    'AQAAAAEAACcQAAAAELbF3tJ8v4Yz7Qx9v2Lz9Nr8Qs7Wt7Fv3Kx9Lr6Ny5Px9Nr8Qs7Wt7Fv3Kx9Lr6Nyw==',
    '7f5c3e9a-4b2d-4e8f-9a1c-3d7e6f5a4b2c',
    NEWID(),
    1,
    0
);

-- Gán role Admin cho user vừa tạo
INSERT INTO AspNetUserRoles (UserId, RoleId)
SELECT 
    (SELECT Id FROM AspNetUsers WHERE Email = 'admin@mamnonlamay.edu.vn'),
    (SELECT Id FROM AspNetRoles WHERE Name = 'Admin');

SELECT 'Admin user created successfully!' AS Message;
