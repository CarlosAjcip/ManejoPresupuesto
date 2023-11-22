--Nombre de la base de Datos = DBpresupuesto

USE [master]
GO
/****** Object:  Database [DBpresupuesto]    Script Date: 21/11/2023 21:37:45 ******/
CREATE DATABASE [DBpresupuesto]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'DBpresuouesto', FILENAME = N'C:\SQLData\DBpresuouesto.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'DBpresuouesto_log', FILENAME = N'C:\SQLData\DBpresuouesto_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [DBpresupuesto] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [DBpresupuesto].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [DBpresupuesto] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [DBpresupuesto] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [DBpresupuesto] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [DBpresupuesto] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [DBpresupuesto] SET ARITHABORT OFF 
GO
ALTER DATABASE [DBpresupuesto] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [DBpresupuesto] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [DBpresupuesto] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [DBpresupuesto] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [DBpresupuesto] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [DBpresupuesto] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [DBpresupuesto] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [DBpresupuesto] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [DBpresupuesto] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [DBpresupuesto] SET  DISABLE_BROKER 
GO
ALTER DATABASE [DBpresupuesto] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [DBpresupuesto] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [DBpresupuesto] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [DBpresupuesto] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [DBpresupuesto] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [DBpresupuesto] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [DBpresupuesto] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [DBpresupuesto] SET RECOVERY FULL 
GO
ALTER DATABASE [DBpresupuesto] SET  MULTI_USER 
GO
ALTER DATABASE [DBpresupuesto] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [DBpresupuesto] SET DB_CHAINING OFF 
GO
ALTER DATABASE [DBpresupuesto] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [DBpresupuesto] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [DBpresupuesto] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [DBpresupuesto] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'DBpresupuesto', N'ON'
GO
ALTER DATABASE [DBpresupuesto] SET QUERY_STORE = ON
GO
ALTER DATABASE [DBpresupuesto] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [DBpresupuesto]
GO
/****** Object:  Table [dbo].[categorias]    Script Date: 21/11/2023 21:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[categorias](
	[id_categorias] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[id_usuarios] [int] NULL,
	[id_tiposOp] [int] NULL,
 CONSTRAINT [PK_categorias] PRIMARY KEY CLUSTERED 
(
	[id_categorias] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Cuentas]    Script Date: 21/11/2023 21:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cuentas](
	[id_cuenta] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Balance] [decimal](18, 2) NULL,
	[Descripcion] [nchar](1000) NULL,
	[id_TiposCuen] [int] NULL,
 CONSTRAINT [PK_Cuentas] PRIMARY KEY CLUSTERED 
(
	[id_cuenta] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposCuentas]    Script Date: 21/11/2023 21:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposCuentas](
	[id_tiposCuen] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](50) NULL,
	[Orden] [int] NULL,
	[id_usuarios] [int] NULL,
 CONSTRAINT [PK_TiposCuentas] PRIMARY KEY CLUSTERED 
(
	[id_tiposCuen] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[TiposOperaciones]    Script Date: 21/11/2023 21:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TiposOperaciones](
	[id_tiposOp] [int] IDENTITY(1,1) NOT NULL,
	[descripcion] [nchar](100) NULL,
 CONSTRAINT [PK_TiposOperaciones] PRIMARY KEY CLUSTERED 
(
	[id_tiposOp] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Transacciones]    Script Date: 21/11/2023 21:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Transacciones](
	[id_transacciones] [int] IDENTITY(1,1) NOT NULL,
	[fechaTransaccion] [datetime] NULL,
	[monto] [decimal](18, 2) NULL,
	[nota] [nvarchar](1000) NULL,
	[id_usuarios] [int] NULL,
	[id_cuenta] [int] NULL,
	[id_categorias] [int] NULL,
 CONSTRAINT [PK_Transacciones] PRIMARY KEY CLUSTERED 
(
	[id_transacciones] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[usuarios]    Script Date: 21/11/2023 21:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[usuarios](
	[id_usarios] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](250) NULL,
	[EmailNormalizado] [nvarchar](250) NULL,
	[PaswordHash] [nvarchar](250) NULL,
 CONSTRAINT [PK_usuarios] PRIMARY KEY CLUSTERED 
(
	[id_usarios] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[categorias] ON 

INSERT [dbo].[categorias] ([id_categorias], [Nombre], [id_usuarios], [id_tiposOp]) VALUES (7, N'Vantas de Libros', 1, 2)
INSERT [dbo].[categorias] ([id_categorias], [Nombre], [id_usuarios], [id_tiposOp]) VALUES (8, N'Salario', 1, 2)
INSERT [dbo].[categorias] ([id_categorias], [Nombre], [id_usuarios], [id_tiposOp]) VALUES (9, N'Comida', 1, 1)
INSERT [dbo].[categorias] ([id_categorias], [Nombre], [id_usuarios], [id_tiposOp]) VALUES (10, N'Gastos Varios', 1, 1)
INSERT [dbo].[categorias] ([id_categorias], [Nombre], [id_usuarios], [id_tiposOp]) VALUES (11, N'TEST', 1, 2)
SET IDENTITY_INSERT [dbo].[categorias] OFF
GO
SET IDENTITY_INSERT [dbo].[Cuentas] ON 

INSERT [dbo].[Cuentas] ([id_cuenta], [Nombre], [Balance], [Descripcion], [id_TiposCuen]) VALUES (1, N'Tarjetas del Banco XX', CAST(50.00 AS Decimal(18, 2)), N'TEST                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ', 39)
INSERT [dbo].[Cuentas] ([id_cuenta], [Nombre], [Balance], [Descripcion], [id_TiposCuen]) VALUES (5, N'Prestamo', CAST(105.00 AS Decimal(18, 2)), N'prestamos de comida                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                     ', 38)
INSERT [dbo].[Cuentas] ([id_cuenta], [Nombre], [Balance], [Descripcion], [id_TiposCuen]) VALUES (6, N'Efectivo', CAST(1580.00 AS Decimal(18, 2)), NULL, 33)
INSERT [dbo].[Cuentas] ([id_cuenta], [Nombre], [Balance], [Descripcion], [id_TiposCuen]) VALUES (7, N'Saldos', CAST(4986.00 AS Decimal(18, 2)), N'test                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    ', 33)
INSERT [dbo].[Cuentas] ([id_cuenta], [Nombre], [Balance], [Descripcion], [id_TiposCuen]) VALUES (8, N'TEST', CAST(780.00 AS Decimal(18, 2)), NULL, 41)
SET IDENTITY_INSERT [dbo].[Cuentas] OFF
GO
SET IDENTITY_INSERT [dbo].[TiposCuentas] ON 

INSERT [dbo].[TiposCuentas] ([id_tiposCuen], [Nombre], [Orden], [id_usuarios]) VALUES (33, N'Efectivo', 4, 1)
INSERT [dbo].[TiposCuentas] ([id_tiposCuen], [Nombre], [Orden], [id_usuarios]) VALUES (36, N'Cuentas de Banco', 3, 1)
INSERT [dbo].[TiposCuentas] ([id_tiposCuen], [Nombre], [Orden], [id_usuarios]) VALUES (38, N'Prestamos', 2, 1)
INSERT [dbo].[TiposCuentas] ([id_tiposCuen], [Nombre], [Orden], [id_usuarios]) VALUES (39, N'Tarjetas', 1, 1)
INSERT [dbo].[TiposCuentas] ([id_tiposCuen], [Nombre], [Orden], [id_usuarios]) VALUES (40, N'Efectivo 2', 5, 1)
INSERT [dbo].[TiposCuentas] ([id_tiposCuen], [Nombre], [Orden], [id_usuarios]) VALUES (41, N'TEST', 6, 1)
SET IDENTITY_INSERT [dbo].[TiposCuentas] OFF
GO
SET IDENTITY_INSERT [dbo].[TiposOperaciones] ON 

INSERT [dbo].[TiposOperaciones] ([id_tiposOp], [descripcion]) VALUES (1, N'Gatos                                                                      Ingresos                 ')
INSERT [dbo].[TiposOperaciones] ([id_tiposOp], [descripcion]) VALUES (2, N'Ingreso                                                                                             ')
SET IDENTITY_INSERT [dbo].[TiposOperaciones] OFF
GO
SET IDENTITY_INSERT [dbo].[Transacciones] ON 

INSERT [dbo].[Transacciones] ([id_transacciones], [fechaTransaccion], [monto], [nota], [id_usuarios], [id_cuenta], [id_categorias]) VALUES (3, CAST(N'2023-11-19T00:00:00.000' AS DateTime), CAST(500.00 AS Decimal(18, 2)), NULL, 1, 1, 10)
INSERT [dbo].[Transacciones] ([id_transacciones], [fechaTransaccion], [monto], [nota], [id_usuarios], [id_cuenta], [id_categorias]) VALUES (4, CAST(N'2023-11-11T00:00:00.000' AS DateTime), CAST(300.00 AS Decimal(18, 2)), NULL, 1, 5, 8)
INSERT [dbo].[Transacciones] ([id_transacciones], [fechaTransaccion], [monto], [nota], [id_usuarios], [id_cuenta], [id_categorias]) VALUES (5, CAST(N'2023-11-19T00:00:00.000' AS DateTime), CAST(5.00 AS Decimal(18, 2)), NULL, 1, 5, 10)
INSERT [dbo].[Transacciones] ([id_transacciones], [fechaTransaccion], [monto], [nota], [id_usuarios], [id_cuenta], [id_categorias]) VALUES (6, CAST(N'2023-11-19T00:00:00.000' AS DateTime), CAST(1000.00 AS Decimal(18, 2)), NULL, 1, 6, 10)
INSERT [dbo].[Transacciones] ([id_transacciones], [fechaTransaccion], [monto], [nota], [id_usuarios], [id_cuenta], [id_categorias]) VALUES (7, CAST(N'2023-11-19T00:00:00.000' AS DateTime), CAST(10.00 AS Decimal(18, 2)), NULL, 1, 6, 9)
INSERT [dbo].[Transacciones] ([id_transacciones], [fechaTransaccion], [monto], [nota], [id_usuarios], [id_cuenta], [id_categorias]) VALUES (8, CAST(N'2023-11-19T00:00:00.000' AS DateTime), CAST(15.00 AS Decimal(18, 2)), NULL, 1, 7, 9)
INSERT [dbo].[Transacciones] ([id_transacciones], [fechaTransaccion], [monto], [nota], [id_usuarios], [id_cuenta], [id_categorias]) VALUES (9, CAST(N'2023-11-19T00:00:00.000' AS DateTime), CAST(1.00 AS Decimal(18, 2)), NULL, 1, 7, 10)
INSERT [dbo].[Transacciones] ([id_transacciones], [fechaTransaccion], [monto], [nota], [id_usuarios], [id_cuenta], [id_categorias]) VALUES (10, CAST(N'2023-11-21T00:00:00.000' AS DateTime), CAST(20.00 AS Decimal(18, 2)), NULL, 1, 6, 10)
INSERT [dbo].[Transacciones] ([id_transacciones], [fechaTransaccion], [monto], [nota], [id_usuarios], [id_cuenta], [id_categorias]) VALUES (11, CAST(N'2023-11-21T00:00:00.000' AS DateTime), CAST(50.00 AS Decimal(18, 2)), NULL, 1, 8, 11)
INSERT [dbo].[Transacciones] ([id_transacciones], [fechaTransaccion], [monto], [nota], [id_usuarios], [id_cuenta], [id_categorias]) VALUES (12, CAST(N'2023-11-21T00:00:00.000' AS DateTime), CAST(70.00 AS Decimal(18, 2)), NULL, 1, 8, 7)
INSERT [dbo].[Transacciones] ([id_transacciones], [fechaTransaccion], [monto], [nota], [id_usuarios], [id_cuenta], [id_categorias]) VALUES (13, CAST(N'2023-10-21T00:00:00.000' AS DateTime), CAST(500.00 AS Decimal(18, 2)), NULL, 1, 8, 11)
SET IDENTITY_INSERT [dbo].[Transacciones] OFF
GO
SET IDENTITY_INSERT [dbo].[usuarios] ON 

INSERT [dbo].[usuarios] ([id_usarios], [Email], [EmailNormalizado], [PaswordHash]) VALUES (1, N'prueba@gmail.com', N'PRUEBA@GMAIl.COM', N'abc')
SET IDENTITY_INSERT [dbo].[usuarios] OFF
GO
ALTER TABLE [dbo].[categorias]  WITH CHECK ADD  CONSTRAINT [FK_categorias_TiposOperaciones] FOREIGN KEY([id_tiposOp])
REFERENCES [dbo].[TiposOperaciones] ([id_tiposOp])
GO
ALTER TABLE [dbo].[categorias] CHECK CONSTRAINT [FK_categorias_TiposOperaciones]
GO
ALTER TABLE [dbo].[categorias]  WITH CHECK ADD  CONSTRAINT [FK_categorias_usuarios] FOREIGN KEY([id_usuarios])
REFERENCES [dbo].[usuarios] ([id_usarios])
GO
ALTER TABLE [dbo].[categorias] CHECK CONSTRAINT [FK_categorias_usuarios]
GO
ALTER TABLE [dbo].[Cuentas]  WITH CHECK ADD  CONSTRAINT [FK_Cuentas_TiposCuentas] FOREIGN KEY([id_TiposCuen])
REFERENCES [dbo].[TiposCuentas] ([id_tiposCuen])
GO
ALTER TABLE [dbo].[Cuentas] CHECK CONSTRAINT [FK_Cuentas_TiposCuentas]
GO
ALTER TABLE [dbo].[TiposCuentas]  WITH CHECK ADD  CONSTRAINT [FK_TiposCuentas_usuarios] FOREIGN KEY([id_usuarios])
REFERENCES [dbo].[usuarios] ([id_usarios])
GO
ALTER TABLE [dbo].[TiposCuentas] CHECK CONSTRAINT [FK_TiposCuentas_usuarios]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_categorias] FOREIGN KEY([id_categorias])
REFERENCES [dbo].[categorias] ([id_categorias])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_categorias]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_Cuentas] FOREIGN KEY([id_cuenta])
REFERENCES [dbo].[Cuentas] ([id_cuenta])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_Cuentas]
GO
ALTER TABLE [dbo].[Transacciones]  WITH CHECK ADD  CONSTRAINT [FK_Transacciones_usuarios] FOREIGN KEY([id_usuarios])
REFERENCES [dbo].[usuarios] ([id_usarios])
GO
ALTER TABLE [dbo].[Transacciones] CHECK CONSTRAINT [FK_Transacciones_usuarios]
GO
/****** Object:  StoredProcedure [dbo].[TiposCuentasInsertar]    Script Date: 21/11/2023 21:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[TiposCuentasInsertar]
	@Nombre nvarchar(50),
	@id_usuarios int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    Declare @Orden int;
	SELECT @Orden = COALESCE(MAX(Orden), 0) +1
	FROM TiposCuentas
	WHERE id_usuarios = @id_usuarios

	INSERT INTO TiposCuentas(Nombre,Orden,id_usuarios)
	VALUES(@Nombre,@Orden,@id_usuarios)

	SELECT SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[Transaccion_Insertar]    Script Date: 21/11/2023 21:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Transaccion_Insertar] 
	-- Add the parameters for the stored procedure here
	@fechaTransaccion date,
	@monto decimal,
	@nota nvarchar(100) = NULL,
	@id_usuarios int,
	@id_cuenta int,
	@id_categorias int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insertando los datos en la tabla transaccion
	INSERT INTO Transacciones(fechaTransaccion,monto,id_usuarios,id_cuenta,id_categorias)
	Values(@fechaTransaccion,ABS(@monto),@id_usuarios,@id_cuenta,@id_categorias)

	--actualizando los datos de la tabla cuentas
	UPDATE Cuentas
	SET Balance += @monto
	WHERE id_cuenta = @id_cuenta;
	
	SELECT SCOPE_IDENTITY();
END
GO
/****** Object:  StoredProcedure [dbo].[Transacciones_Actualizar]    Script Date: 21/11/2023 21:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Transacciones_Actualizar] 
	-- Add the parameters for the stored procedure here
	@id_transacciones int,
	@fechaTransaccion datetime,
	@monto decimal(18,2),
	@MontoAnterior decimal(18,2),
	@id_cuenta int,
	@cuentaAnteriorId int,
	@id_categorias int,
	@nota nvarchar(1000) = NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Revertir la transaccions anterior
	UPDATE Cuentas
	SET Balance -= @MontoAnterior
	WHERE id_cuenta = @cuentaAnteriorId;

	--Realizar la nueva Transaccion
	UPDATE Cuentas
	SET Balance += @monto
	WHERE id_cuenta = @id_cuenta;

	UPDATE Transacciones
	SET monto = ABS(@monto), fechaTransaccion = @fechaTransaccion,id_categorias = @id_categorias,id_cuenta = @id_cuenta,nota = @nota
	WHERE id_transacciones = @id_transacciones;
END
GO
/****** Object:  StoredProcedure [dbo].[Transacciones_Borrar]    Script Date: 21/11/2023 21:37:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[Transacciones_Borrar]
	-- Add the parameters for the stored procedure here
	@id_transacciones int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @monto decimal(18,2);
	DECLARE @id_cuenta int;
	DECLARE @TipoOperacionId int;

	SELECT @monto = monto, @id_cuenta = id_cuenta, @TipoOperacionId = cat.id_tiposOp
	FROM Transacciones
	inner join categorias cat
	on cat.id_categorias = Transacciones.id_categorias
	where Transacciones.id_transacciones = @id_transacciones;

	DECLARE @FactorMultiplicativo int = 1;

	IF(@TipoOperacionId = 2)
		SET @FactorMultiplicativo = -1;

	set @monto = @monto * @FactorMultiplicativo;

	UPDATE Cuentas
	SET Balance -= @monto
	WHERE id_cuenta = @id_cuenta;

	DELETE Transacciones
	WHERE id_transacciones = @id_transacciones;

END
GO
USE [master]
GO
ALTER DATABASE [DBpresupuesto] SET  READ_WRITE 
GO
