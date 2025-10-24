```sql
USE [DAT_Dev]
GO

/****** Object:  Table [dbo].[Tbl_Product]    Script Date: 10/23/2025 1:58:23 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Tbl_Product](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](50) NOT NULL,
	[Price] [decimal](20, 2) NOT NULL,
	[Quantity] [int] NOT NULL,
	[CreatedBy] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](50) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsDelete] [bit] NOT NULL,
 CONSTRAINT [PK_Tbl_Product] PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
```

```sql
INSERT INTO [dbo].[Tbl_Product] (
    [ProductName],
    [Price],
    [Quantity],
    [CreatedBy],
    [CreatedDate],
    [ModifiedBy],
    [ModifiedDate],
    [IsDelete]
)
VALUES 
('Wireless Mouse', 25.99, 100, 'admin', GETDATE(), NULL, NULL, 0),
('Mechanical Keyboard', 89.50, 50, 'admin', GETDATE(), NULL, NULL, 0),
('USB-C Hub', 45.00, 75, 'admin', GETDATE(), NULL, NULL, 0),
('Laptop Stand', 39.99, 120, 'admin', GETDATE(), NULL, NULL, 0),
('Webcam 1080p', 59.95, 80, 'admin', GETDATE(), NULL, NULL, 0);
```

dotnet ef dbcontext scaffold "Server=.;Database=DAT_Dev;User ID=sa;Password=sasa@123;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -o CLAppDbContextModels -c CLAppDbContext