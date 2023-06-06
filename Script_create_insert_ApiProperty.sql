-----------------------------------------CREATE TABLE

CREATE TABLE [dbo].[Owner](
	[IdOwner] [varchar](100) NOT NULL,
	[Name] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[Photo] [nvarchar](max) NULL,
	[Birthday] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdOwner] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]


CREATE TABLE [dbo].[Property](
	[IdProperty] [varchar](100) NOT NULL,
	[Name] [varchar](100) NULL,
	[Address] [varchar](100) NULL,
	[Price] [decimal](20, 2) NULL,
	[CodeInternal] [varchar](100) NULL,
	[Year] [int] NULL,
	[IdOwner] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdProperty] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Property]  WITH CHECK ADD FOREIGN KEY([IdOwner])
REFERENCES [dbo].[Owner] ([IdOwner])

CREATE TABLE [dbo].[PropertyImage](
	[IdPropertyImage] [varchar](100) NOT NULL,
	[IdProperty] [varchar](100) NULL,
	[File] [nvarchar](max) NULL,
	[Enabled] [bit] NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPropertyImage] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[PropertyImage]  WITH CHECK ADD FOREIGN KEY([IdProperty])
REFERENCES [dbo].[Property] ([IdProperty])


CREATE TABLE [dbo].[PropertyTrace](
	[IdPropertyTrace] [varchar](100) NOT NULL,
	[DateSale] [datetime] NULL,
	[Name] [varchar](100) NULL,
	[Value] [decimal](20, 2) NULL,
	[Tax] [varchar](100) NULL,
	[IdProperty] [varchar](100) NULL,
PRIMARY KEY CLUSTERED 
(
	[IdPropertyTrace] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[PropertyTrace]  WITH CHECK ADD FOREIGN KEY([IdProperty])
REFERENCES [dbo].[Property] ([IdProperty])


----------------------------------------------------INSERT

INSERT INTO [dbo].[Owner]
           ([IdOwner], [Name], [Address], [Photo]
           ,[Birthday])
     VALUES
           ('OW1','Person','5TH Avenue, 35th Floor',
           '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',
           GETDATE())

INSERT INTO [dbo].[Property]
           ([IdProperty], [Name], [Address], [Price], [CodeInternal], [Year], [IdOwner])
     VALUES
           ('PROP1', 'House77', '1TH Avenue, 44th Floor', 400000000.00, 'COD1', 2001, 'OW1')

INSERT INTO [dbo].[PropertyImage]
           ([IdPropertyImage], [IdProperty], [File], [Enabled])
     VALUES
           ('PROPIM1', 'PROP1', '/9j/4AAQSkZJRgABAQAAAQABAAD/2wCEAAMDAwMDAwQEBAQFBQUFBQcHBgYHBwsICQgJCAsRCwwLCwwLEQ8SDw4PEg8bFRMTFRsfGhkaHyYiIiYwLTA+', 1)