USE post_db;
GO

-- 1. මුලින්ම පරණ සම්බන්ධතා (Foreign Keys) ඔක්කොම කඩලා දාමු
DECLARE @sql NVARCHAR(MAX) = N'';
SELECT @sql += 'ALTER TABLE ' + QUOTENAME(OBJECT_SCHEMA_NAME(parent_object_id))
    + '.' + QUOTENAME(OBJECT_NAME(parent_object_id)) 
    + ' DROP CONSTRAINT ' + QUOTENAME(name) + ';'
FROM sys.foreign_keys;
EXEC sp_executesql @sql;
GO

-- 2. තියෙන ඔක්කොම ටේබල් අයින් කරනවා
DROP TABLE IF EXISTS dbo.TeleMailDetails;
DROP TABLE IF EXISTS dbo.TeleMailDetail;
DROP TABLE IF EXISTS dbo.Receipt;
DROP TABLE IF EXISTS dbo.CashBook;
DROP TABLE IF EXISTS dbo.MailItem;
DROP TABLE IF EXISTS dbo.BungalowBooking;
DROP TABLE IF EXISTS dbo.StampOrder;
DROP TABLE IF EXISTS dbo.Payment;
DROP TABLE IF EXISTS dbo.ServiceRequest;
DROP TABLE IF EXISTS dbo.PostOfficer;
DROP TABLE IF EXISTS dbo.Customer;
DROP TABLE IF EXISTS dbo.Person;
DROP TABLE IF EXISTS dbo.__EFMigrationsHistory;
GO

-- 3. දැන් පිළිවෙලට ටේබල් ටික හදමු

-- Person Table
CREATE TABLE Person (
    PersonId INT PRIMARY KEY IDENTITY(1,1),
    FullName NVARCHAR(MAX) NOT NULL,
    Address NVARCHAR(MAX),
    ContactNumber NVARCHAR(MAX) NOT NULL
);

-- PostOfficer Table
CREATE TABLE PostOfficer (
    OfficerId INT PRIMARY KEY,
    Username NVARCHAR(MAX) NOT NULL,
    Password NVARCHAR(MAX) NOT NULL, 
    CONSTRAINT FK_PostOfficer_Person FOREIGN KEY (OfficerId) REFERENCES Person(PersonId)
);

-- Customer Table
CREATE TABLE Customer (
    CustomerId INT PRIMARY KEY,
    Email NVARCHAR(MAX),
    CONSTRAINT FK_Customer_Person FOREIGN KEY (CustomerId) REFERENCES Person(PersonId)
);

-- ServiceRequest Table (Sprint 1)
CREATE TABLE ServiceRequest (
    Id INT PRIMARY KEY IDENTITY(1,1),
    ReferenceNumber NVARCHAR(MAX) NULL, 
    ServiceType NVARCHAR(MAX) NOT NULL,
    SenderName NVARCHAR(MAX) NOT NULL,
    SenderPhone NVARCHAR(MAX) NOT NULL,
    ReceiverName NVARCHAR(MAX) NOT NULL,
    ReceiverPhone NVARCHAR(MAX) NOT NULL,
    Destination NVARCHAR(MAX) NOT NULL,
    Weight DECIMAL(18,2) NOT NULL,
    Price DECIMAL(18,2) NOT NULL,
    Status NVARCHAR(MAX) NOT NULL DEFAULT 'PENDING',
    CreatedDate DATETIME2 DEFAULT GETDATE(),
    PostOfficerOfficerId INT NULL,
    CONSTRAINT FK_ServiceRequest_Officer FOREIGN KEY (PostOfficerOfficerId) REFERENCES PostOfficer(OfficerId)
);

-- MailItem Table
CREATE TABLE MailItem (
    ItemId INT PRIMARY KEY IDENTITY(1,1),
    RequestId INT NOT NULL,
    ItemType NVARCHAR(MAX), 
    Weight DECIMAL(18,2),
    Destination NVARCHAR(MAX),
    CONSTRAINT FK_MailItem_Request FOREIGN KEY (RequestId) REFERENCES ServiceRequest(Id)
);

-- TeleMailDetail Table
CREATE TABLE TeleMailDetail (
    TeleMailId INT PRIMARY KEY IDENTITY(1,1),
    RequestId INT NOT NULL,
    MessageContent NVARCHAR(MAX),
    ReceiverName NVARCHAR(MAX),
    ReceiverAddress NVARCHAR(MAX),
    CONSTRAINT FK_TeleMail_Request FOREIGN KEY (RequestId) REFERENCES ServiceRequest(Id)
);

-- Payment Table
CREATE TABLE Payment (
    PaymentId INT PRIMARY KEY IDENTITY(1,1),
    RequestId INT NOT NULL,
    Amount DECIMAL(18,2) NOT NULL,
    PaymentType NVARCHAR(MAX),
    PaymentDate DATETIME2 DEFAULT GETDATE(),
    CONSTRAINT FK_Payment_Request FOREIGN KEY (RequestId) REFERENCES ServiceRequest(Id)
);

-- Receipt Table
CREATE TABLE Receipt (
    ReceiptId INT PRIMARY KEY IDENTITY(1,1),
    PaymentId INT NOT NULL,
    ReceiptNumber NVARCHAR(MAX),
    GeneratedDate DATETIME2 DEFAULT GETDATE(),
    CONSTRAINT FK_Receipt_Payment FOREIGN KEY (PaymentId) REFERENCES Payment(PaymentId)
);

-- CashBook Table
CREATE TABLE CashBook (
    EntryId INT PRIMARY KEY IDENTITY(1,1),
    PaymentId INT NULL,
    Amount DECIMAL(18,2) NULL,
    TransactionType NVARCHAR(MAX) DEFAULT 'IN', 
    EntryDate DATETIME2 DEFAULT GETDATE(),
    CONSTRAINT FK_CashBook_Payment FOREIGN KEY (PaymentId) REFERENCES Payment(PaymentId)
);

-- BungalowBooking Table
CREATE TABLE BungalowBooking (
    BookingId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NULL,
    BungalowName NVARCHAR(MAX),
    CheckInDate DATE,
    CheckOutDate DATE,
    Status NVARCHAR(MAX) DEFAULT 'BOOKED',
    CONSTRAINT FK_Bungalow_Customer FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);

-- StampOrder Table
CREATE TABLE StampOrder (
    OrderId INT PRIMARY KEY IDENTITY(1,1),
    CustomerId INT NULL,
    DesignFilePath NVARCHAR(MAX),
    Quantity INT NULL,
    OrderDate DATETIME2 DEFAULT GETDATE(),
    CONSTRAINT FK_Stamp_Customer FOREIGN KEY (CustomerId) REFERENCES Customer(CustomerId)
);
GO