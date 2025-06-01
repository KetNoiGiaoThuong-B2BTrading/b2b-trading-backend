USE master;
GO
-- Xóa database nếu đã tồn tại
IF EXISTS (SELECT * FROM sys.databases WHERE name = 'B2BTrading')
BEGIN
    ALTER DATABASE B2BTrading SET SINGLE_USER WITH ROLLBACK IMMEDIATE;
    DROP DATABASE B2BTrading;
END
GO

-- Tạo database mới
CREATE DATABASE B2BTrading;
GO

USE B2BTrading;
GO

-- Tạo bảng Company (Công ty)
CREATE TABLE Company (
    CompanyID INT IDENTITY(1,1) PRIMARY KEY,
    CompanyName NVARCHAR(200) NOT NULL,
    TaxCode NVARCHAR(50) NOT NULL,
    BusinessSector NVARCHAR(200) NULL,
    Address NVARCHAR(300) NULL,
    Representative NVARCHAR(100) NULL,
    Email NVARCHAR(100) NULL,
    PhoneNumber NVARCHAR(20) NULL,
    RegistrationDate DATETIME NOT NULL DEFAULT GETDATE(),
    VerificationStatus NVARCHAR(50) NULL,--   Chưa xác minh || Đã xác minh
    LegalDocuments NVARCHAR(MAX) NULL,
	ImageCompany NVARCHAR(MAX) NULL,
);
GO

-- Tạo bảng CompanyDocument (Hồ sơ pháp lý công ty)
CREATE TABLE CompanyDocument (
    DocumentID INT IDENTITY(1,1) PRIMARY KEY,
    CompanyID INT NOT NULL,
    DocumentType NVARCHAR(50) NOT NULL,
    FilePath NVARCHAR(500) NOT NULL,
    UploadDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_CompanyDocument_Company FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID) ON DELETE CASCADE
);
GO

-- Tạo bảng UserAccount (Người dùng)
CREATE TABLE UserAccount (
    UserID INT IDENTITY(1,1) PRIMARY KEY,
    CompanyID INT NOT NULL,
    FullName NVARCHAR(100) NOT NULL,
    Email NVARCHAR(100) NOT NULL UNIQUE,
    Password NVARCHAR(255) NOT NULL,
    Role NVARCHAR(50) NOT NULL CHECK (Role IN (N'Admin', N'CompanyManager', N'CompanyStaff', N'ApprovalUser')),
    Status NVARCHAR(20) NOT NULL DEFAULT 'Active',
    CONSTRAINT FK_UserAccount_Company FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID) ON DELETE CASCADE
);
GO

-- Tạo bảng Category (Danh mục sản phẩm)
CREATE TABLE Category (
    CategoryID INT IDENTITY(1,1) PRIMARY KEY,
    CategoryName NVARCHAR(100) NOT NULL,
    ParentCategoryID INT NULL,
	ImageCategoly NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Category_ParentCategory FOREIGN KEY (ParentCategoryID) REFERENCES Category(CategoryID)
);
GO

-- Tạo bảng Product (Sản phẩm)
CREATE TABLE Product (
    ProductID INT IDENTITY(1,1) PRIMARY KEY,
    CompanyID INT NOT NULL,
    CategoryID INT NOT NULL,
    ProductName NVARCHAR(200) NOT NULL,
    Description NVARCHAR(MAX) NULL,
    UnitPrice DECIMAL(18,2) NOT NULL,
    StockQuantity INT NOT NULL DEFAULT 0,
    Status NVARCHAR(50) NOT NULL DEFAULT 'Available',
    Image NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    ApprovedBy INT NULL,
    ApprovalNotes NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Product_Company FOREIGN KEY (CompanyID) REFERENCES Company(CompanyID) ON DELETE CASCADE,
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID) REFERENCES Category(CategoryID),
    CONSTRAINT FK_Product_ApprovedBy FOREIGN KEY (ApprovedBy) REFERENCES UserAccount(UserID)
);
GO

-- Tạo bảng QuotationRequest (Yêu cầu báo giá)
CREATE TABLE QuotationRequest (
    RequestID INT IDENTITY(1,1) PRIMARY KEY,
    BuyerCompanyID INT NOT NULL,
    SellerCompanyID INT NOT NULL,
    ProductID INT NOT NULL,
    Quantity INT NOT NULL,
    DeliveryTime NVARCHAR(100) NULL,
    AdditionalRequest NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_QuotationRequest_BuyerCompany FOREIGN KEY (BuyerCompanyID) REFERENCES Company(CompanyID),
    CONSTRAINT FK_QuotationRequest_SellerCompany FOREIGN KEY (SellerCompanyID) REFERENCES Company(CompanyID),
    CONSTRAINT FK_QuotationRequest_Product FOREIGN KEY (ProductID) REFERENCES Product(ProductID)
);
GO

-- Tạo bảng QuotationResponse (Phản hồi báo giá)
CREATE TABLE QuotationResponse (
    ResponseID INT IDENTITY(1,1) PRIMARY KEY,
    RequestID INT NOT NULL,
    BuyerCompanyID INT NOT NULL,
    SellerCompanyID INT NOT NULL,
    ProposedPrice DECIMAL(18,2) NOT NULL,
    DeliveryTime NVARCHAR(100) NULL,
    ShippingMethod NVARCHAR(100) NULL,
    Terms NVARCHAR(MAX) NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_QuotationResponse_Request FOREIGN KEY (RequestID) REFERENCES QuotationRequest(RequestID),
    CONSTRAINT FK_QuotationResponse_BuyerCompany FOREIGN KEY (BuyerCompanyID) REFERENCES Company(CompanyID),
    CONSTRAINT FK_QuotationResponse_SellerCompany FOREIGN KEY (SellerCompanyID) REFERENCES Company(CompanyID)
);
GO

-- Tạo bảng Contract (Hợp đồng)
CREATE TABLE Contract (
    ContractID INT IDENTITY(1,1) PRIMARY KEY,
    SellerCompanyID INT NOT NULL,
    BuyerCompanyID INT NOT NULL,
    ContractType NVARCHAR(50) NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Terms NVARCHAR(MAX) NOT NULL,
    DisputeResolution NVARCHAR(MAX) NULL,
    Status NVARCHAR(50) NOT NULL CHECK (Status IN (N'PendingSign', N'Signed', N'InProgress', N'Completed', N'Cancelled', N'Disputed', N'Expired')),
    SellerSignature NVARCHAR(MAX) NULL,
    BuyerSignature NVARCHAR(MAX) NULL,
    SignDate DATETIME NULL,
    ContractFile NVARCHAR(MAX) NULL,
    DigitalSignature BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Contract_SellerCompany FOREIGN KEY (SellerCompanyID) REFERENCES Company(CompanyID),
    CONSTRAINT FK_Contract_BuyerCompany FOREIGN KEY (BuyerCompanyID) REFERENCES Company(CompanyID)
);
GO

-- Tạo bảng PeriodicTransaction (Giao dịch định kỳ)
CREATE TABLE PeriodicTransaction (
    TransactionID INT IDENTITY(1,1) PRIMARY KEY,
    ContractID INT NOT NULL,
    DueDate DATE NOT NULL,
    Status NVARCHAR(50) NOT NULL,
    InvoiceFile NVARCHAR(MAX) NULL,
    ReportFile NVARCHAR(MAX) NULL,
    CONSTRAINT FK_PeriodicTransaction_Contract FOREIGN KEY (ContractID) REFERENCES Contract(ContractID)
);
GO

-- Tạo bảng InvestmentRound (Giai đoạn đầu tư)
CREATE TABLE InvestmentRound (
    RoundID INT IDENTITY(1,1) PRIMARY KEY,
    ContractID INT NOT NULL,
    ProjectName NVARCHAR(200) NOT NULL,
    StageName NVARCHAR(200) NOT NULL,
    InvestmentRate FLOAT NOT NULL,
    InvestmentAmount DECIMAL(18,2) NOT NULL,
    ProfitCommitment FLOAT NOT NULL,
    PlannedStartDate DATE NOT NULL,
    PlannedEndDate DATE NOT NULL,
    ActualStartDate DATE NULL,
    ActualEndDate DATE NULL,
    Progress FLOAT CHECK (Progress >= 0 AND Progress <= 100) NOT NULL DEFAULT 0,
    Status NVARCHAR(50) NOT NULL CHECK (Status IN (N'NotStarted', N'InProgress', N'Completed', N'Paused', N'Cancelled')),
    ActualRevenue DECIMAL(18,2) NULL,
    ActualProfit DECIMAL(18,2) NULL,
    Notes NVARCHAR(MAX) NULL,
    CONSTRAINT FK_InvestmentRound_Contract FOREIGN KEY (ContractID) REFERENCES Contract(ContractID)
);
GO

-- Tạo bảng Payment (Thanh toán)
CREATE TABLE Payment (
    PaymentID INT IDENTITY(1,1) PRIMARY KEY,
    ContractID INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentDate DATETIME NOT NULL DEFAULT GETDATE(),
    Method NVARCHAR(100) NOT NULL CHECK (Method IN (N'BankTransfer', N'Cash', N'EWallet', N'CreditCard', N'COD', N'QRCode')),
    Status NVARCHAR(50) NOT NULL,
    Notes NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Payment_Contract FOREIGN KEY (ContractID) REFERENCES Contract(ContractID)
);
GO

-- Tạo bảng Shipment (Vận chuyển)
CREATE TABLE Shipment (
    ShipmentID INT IDENTITY(1,1) PRIMARY KEY,
    ContractID INT NOT NULL,
    Status NVARCHAR(100) NOT NULL,
    UpdateDate DATETIME NOT NULL DEFAULT GETDATE(),
    Description NVARCHAR(MAX) NULL,
    CONSTRAINT FK_Shipment_Contract FOREIGN KEY (ContractID) REFERENCES Contract(ContractID)
);
GO

-- Tạo bảng Review (Đánh giá)
CREATE TABLE Review (
    ReviewID INT IDENTITY(1,1) PRIMARY KEY,
    SenderCompanyID INT NOT NULL,
    ReceiverCompanyID INT NOT NULL,
    ContractID INT NOT NULL,
    Rating INT NOT NULL CHECK (Rating >= 1 AND Rating <= 5),
    Comment NVARCHAR(MAX) NULL,
    ReviewDate DATETIME NOT NULL DEFAULT GETDATE(),
    CONSTRAINT FK_Review_SenderCompany FOREIGN KEY (SenderCompanyID) REFERENCES Company(CompanyID),
    CONSTRAINT FK_Review_ReceiverCompany FOREIGN KEY (ReceiverCompanyID) REFERENCES Company(CompanyID),
    CONSTRAINT FK_Review_Contract FOREIGN KEY (ContractID) REFERENCES Contract(ContractID)
);
GO

-- Tạo bảng TransactionHistory (Lịch sử giao dịch)
CREATE TABLE TransactionHistory (
    HistoryID INT IDENTITY(1,1) PRIMARY KEY,
    ContractID INT NOT NULL,
    Action NVARCHAR(100) NOT NULL,
    PerformedByUserID INT NOT NULL,
    ActionTime DATETIME NOT NULL DEFAULT GETDATE(),
    Notes NVARCHAR(MAX) NULL,
    CONSTRAINT FK_TransactionHistory_Contract FOREIGN KEY (ContractID) REFERENCES Contract(ContractID),
    CONSTRAINT FK_TransactionHistory_User FOREIGN KEY (PerformedByUserID) REFERENCES UserAccount(UserID)
);
GO

-- Tạo bảng Notification (Thông báo)
CREATE TABLE Notification (
    NotificationID INT IDENTITY(1,1) PRIMARY KEY,
    UserID INT NOT NULL,
    Title NVARCHAR(200) NOT NULL,
    Content NVARCHAR(MAX) NOT NULL,
    CreatedDate DATETIME NOT NULL DEFAULT GETDATE(),
    IsRead BIT NOT NULL DEFAULT 0,
    CONSTRAINT FK_Notification_User FOREIGN KEY (UserID) REFERENCES UserAccount(UserID)
);
GO

-- ===========================
-- Thêm dữ liệu mẫu chi tiết
-- ===========================
-- Dữ liệu mẫu bảng Company
INSERT INTO Company (CompanyName, TaxCode, BusinessSector, Address, Representative, Email, PhoneNumber, RegistrationDate, VerificationStatus, LegalDocuments, ImageCompany)
VALUES
(N'Công ty TNHH Công nghệ ABC', N'0102030405', N'Công nghệ thông tin', N'123 Đường Công nghệ, TP.HCM', N'Nguyễn Văn A', N'contact@abc-tech.com', N'0909123456', GETDATE(), N'Đã xác minh', N'Link tới hồ sơ pháp lý ABC',N'1.jpg'),
(N'Công ty CP Sản xuất XYZ', N'0203040506', N'Sản xuất và phân phối', N'456 Đường Sản xuất, Hà Nội', N'Trần Thị B', N'info@xyzproduction.vn', N'0912233445', DATEADD(day, -30, GETDATE()), N'Chưa xác minh', N'Link tới hồ sơ pháp lý XYZ',N'1.jpg'),
(N'Công ty TM DV DEF', N'0304050607', N'Thương mại dịch vụ', N'789 Đường Thương mại, Đà Nẵng', N'Lê Văn C', N'support@defservices.vn', N'0988877665', DATEADD(day, -10, GETDATE()), N'Đã xác minh', N'Link tới hồ sơ pháp lý DEF',N'1.jpg');

-- Dữ liệu mẫu bảng UserAccount
INSERT INTO UserAccount (CompanyID, FullName, Email, Password, Role, Status)
VALUES
(1, N'Nguyễn Văn A', N'admin@abc-tech.com', N'hashedpassword1', N'Admin', N'Active'),
(1, N'Trần Văn B', N'manager@abc-tech.com', N'hashedpassword2', N'CompanyManager', N'Active'),
(2, N'Phạm Thị C', N'staff@xyzproduction.vn', N'hashedpassword3', N'CompanyStaff', N'Active'),
(3, N'Lê Văn D', N'approver@defservices.vn', N'hashedpassword4', N'ApprovalUser', N'Active');

-- Dữ liệu mẫu bảng Category
INSERT INTO Category (CategoryName, ParentCategoryID, ImageCategoly)
VALUES
(N'Điện tử', NULL, N'1.jpg'),
(N'Điện thoại', 1,N'1.jpg'),
(N'Laptop', 1,N'1.jpg'),
(N'Phụ kiện', NULL,N'1.jpg'),
(N'Sạc điện thoại', 4,N'1.jpg');

-- Dữ liệu mẫu bảng Product
INSERT INTO Product (CompanyID, CategoryID, ProductName, Description, UnitPrice, StockQuantity, Status, Image, CreatedDate)
VALUES
(1, 2, N'Điện thoại ABC Model X', N'Điện thoại thông minh ABC Model X với màn hình 6.5 inch', 7500000, 100, N'Available', N'link_image_abc_model_x.jpg', GETDATE()),
(1, 3, N'Laptop ABC Pro', N'Laptop hiệu năng cao cho dân văn phòng và lập trình', 15000000, 50, N'Available', N'link_image_abc_laptop_pro.jpg', GETDATE()),
(2, 5, N'Sạc nhanh XYZ 20W', N'Sạc nhanh chuẩn QC 3.0 cho điện thoại và tablet', 250000, 200, N'Available', N'link_image_xyz_charger.jpg', GETDATE());

-- Dữ liệu mẫu bảng QuotationRequest
INSERT INTO QuotationRequest (BuyerCompanyID, SellerCompanyID, ProductID, Quantity, DeliveryTime, AdditionalRequest, CreatedDate)
VALUES
(2, 1, 1, 500, N'15 ngày', N'Giao hàng nhanh nếu có thể', GETDATE()),
(3, 1, 2, 30, N'1 tháng', N'Bảo hành 12 tháng', DATEADD(day, -5, GETDATE()));

-- Dữ liệu mẫu bảng QuotationResponse
INSERT INTO QuotationResponse (RequestID, BuyerCompanyID, SellerCompanyID, ProposedPrice, DeliveryTime, ShippingMethod, Terms, CreatedDate)
VALUES
(1, 2, 1, 7200000, N'14 ngày', N'Vận chuyển đường bộ', N'Thanh toán trước 50%', GETDATE()),
(2, 3, 1, 14800000, N'30 ngày', N'Vận chuyển đường biển', N'Thanh toán sau khi nhận hàng', DATEADD(day, -1, GETDATE()));

-- Dữ liệu mẫu bảng Contract
INSERT INTO Contract (SellerCompanyID, BuyerCompanyID, ContractType, Title, Terms, DisputeResolution, Status, SellerSignature, BuyerSignature, SignDate, ContractFile, DigitalSignature)
VALUES
(1, 2, N'Mua bán hàng hóa', N'Hợp đồng mua bán điện thoại ABC', N'Điều khoản thanh toán: chuyển khoản trước 50%, giao hàng trong 15 ngày', N'Giải quyết tranh chấp tại Tòa án TP.HCM', N'Signed', N'Nguyễn Văn A', N'Trần Thị B', GETDATE(), N'link_contract_file.pdf', 1),
(1, 3, N'Mua bán hàng hóa', N'Hợp đồng mua bán laptop ABC', N'Thanh toán đầy đủ trước khi giao hàng', N'Hòa giải tại Trung tâm Trọng tài Việt Nam', N'InProgress', NULL, NULL, NULL, NULL, 0);

-- Dữ liệu mẫu bảng PeriodicTransaction
INSERT INTO PeriodicTransaction (ContractID, DueDate, Status, InvoiceFile, ReportFile)
VALUES
(1, DATEADD(month, 1, GETDATE()), N'Pending', NULL, NULL),
(1, DATEADD(month, 2, GETDATE()), N'Completed', N'invoice_001.pdf', N'report_001.pdf');

-- Dữ liệu mẫu bảng InvestmentRound
INSERT INTO InvestmentRound (ContractID, ProjectName, StageName, InvestmentRate, InvestmentAmount, ProfitCommitment, PlannedStartDate, PlannedEndDate, ActualStartDate, ActualEndDate, Progress, Status, ActualRevenue, ActualProfit, Notes)
VALUES
(1, N'Dự án phát triển sản phẩm ABC', N'Giai đoạn seed', 10.5, 5000000000, 20, '2025-01-01', '2025-06-30', '2025-01-05', NULL, 40, N'InProgress', 2000000000, 400000000, N'Tiến độ đúng kế hoạch'),
(2, N'Dự án mở rộng sản xuất XYZ', N'Giai đoạn Series A', 25, 20000000000, 15, '2025-03-01', '2025-12-31', NULL, NULL, 0, N'NotStarted', NULL, NULL, N'Chưa bắt đầu');

-- Dữ liệu mẫu bảng Payment
INSERT INTO Payment (ContractID, Amount, PaymentDate, Method, Status, Notes)
VALUES
(1, 100000000, GETDATE(), N'BankTransfer', N'Completed', N'Thanh toán lần 1'),
(1, 200000000, DATEADD(day, -10, GETDATE()), N'EWallet', N'Completed', N'Thanh toán lần 2'),
(2, 500000000, GETDATE(), N'CreditCard', N'Pending', N'Thanh toán chưa xác nhận');

-- Dữ liệu mẫu bảng Shipment
INSERT INTO Shipment (ContractID, Status, UpdateDate, Description)
VALUES
(1, N'Đang giao hàng', GETDATE(), N'Đơn hàng đã xuất kho, dự kiến giao trong 5 ngày'),
(2, N'Chưa giao hàng', DATEADD(day, -2, GETDATE()), N'Chờ xác nhận thanh toán');

-- Dữ liệu mẫu bảng Review
INSERT INTO Review (SenderCompanyID, ReceiverCompanyID, ContractID, Rating, Comment, ReviewDate)
VALUES
(2, 1, 1, 5, N'Sản phẩm chất lượng, giao hàng đúng hạn.', GETDATE()),
(3, 1, 2, 4, N'Dịch vụ tốt nhưng giao hàng hơi chậm.', DATEADD(day, -1, GETDATE()));

-- Dữ liệu mẫu bảng TransactionHistory
INSERT INTO TransactionHistory (ContractID, Action, PerformedByUserID, ActionTime, Notes)
VALUES
(1, N'Tạo hợp đồng', 1, DATEADD(day, -20, GETDATE()), N'Hợp đồng ký ngày 15/05/2025'),
(1, N'Thanh toán', 2, DATEADD(day, -5, GETDATE()), N'Thanh toán lần 1 qua chuyển khoản'),
(2, N'Tạo yêu cầu báo giá', 3, DATEADD(day, -7, GETDATE()), N'Yêu cầu báo giá laptop ABC');

-- Dữ liệu mẫu bảng Notification
INSERT INTO Notification (UserID, Title, Content, CreatedDate, IsRead)
VALUES
(1, N'Yêu cầu báo giá mới', N'Bạn có một yêu cầu báo giá mới từ Công ty XYZ.', GETDATE(), 0),
(2, N'Hợp đồng đã được ký', N'Hợp đồng mua bán điện thoại ABC đã được ký thành công.', DATEADD(hour, -3, GETDATE()), 1);
