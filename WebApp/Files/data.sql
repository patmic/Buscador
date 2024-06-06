USE [CAN_DB]
GO
SET IDENTITY_INSERT [dbo].[DataLake] ON 
GO
INSERT [dbo].[DataLake] ([IdDataLake], [DataTipo], [DataSistemaOrigen], [DataSistemaOrigenId], [DataSistemaFecha], [Estado], [DataFechaCarga]) VALUES (5, N'ORGANIZACION', N'SAE', N'1', CAST(N'2024-02-22T00:00:00.000' AS DateTime), N'A', CAST(N'2024-06-03T22:19:19.680' AS DateTime))
GO
INSERT [dbo].[DataLake] ([IdDataLake], [DataTipo], [DataSistemaOrigen], [DataSistemaOrigenId], [DataSistemaFecha], [Estado], [DataFechaCarga]) VALUES (6, N'ORGANIZACION', N'SAE', N'2', NULL, N'A', CAST(N'2024-06-03T22:20:02.620' AS DateTime))
GO
INSERT [dbo].[DataLake] ([IdDataLake], [DataTipo], [DataSistemaOrigen], [DataSistemaOrigenId], [DataSistemaFecha], [Estado], [DataFechaCarga]) VALUES (7, N'ORGANIZACION', N'SAE', N'3', NULL, N'A', CAST(N'2024-06-03T22:20:11.800' AS DateTime))
GO
INSERT [dbo].[DataLake] ([IdDataLake], [DataTipo], [DataSistemaOrigen], [DataSistemaOrigenId], [DataSistemaFecha], [Estado], [DataFechaCarga]) VALUES (8, N'ORGANIZACION', N'SAE', N'4', NULL, N'A', CAST(N'2024-06-03T22:20:34.147' AS DateTime))
GO
SET IDENTITY_INSERT [dbo].[DataLake] OFF
GO
SET IDENTITY_INSERT [dbo].[DataLakeOrganizacion] ON 
GO
INSERT [dbo].[DataLakeOrganizacion] ([IdDataLakeOrganizacion], [IdHomologacionEsquema], [IdDataLake], [DataEsquemaJson], [DataFechaCarga], [FechaCreacion], [Estado]) VALUES (5, 2, 5, N'[
    {"IdHomologacion": "41", "Data": "41 idHomologacion de dato"},
    {"IdHomologacion": "42", "Data": "42 idHomologacion de dato"},
    {"IdHomologacion": "43", "Data": "43 idHomologacion de dato"},
    {"IdHomologacion": "44", "Data": "44 idHomologacion de dato"},
    {"IdHomologacion": "46", "Data": "46 idHomologacion de dato"}
]', CAST(N'2024-06-03T22:21:47.790' AS DateTime), CAST(N'2024-06-03T22:21:47.790' AS DateTime), N'A')
GO
INSERT [dbo].[DataLakeOrganizacion] ([IdDataLakeOrganizacion], [IdHomologacionEsquema], [IdDataLake], [DataEsquemaJson], [DataFechaCarga], [FechaCreacion], [Estado]) VALUES (6, 2, 6, N'[
    {"IdHomologacion": "41", "Data": "41 idHomologacion de dato"},
    {"IdHomologacion": "42", "Data": "42 idHomologacion de dato"},
    {"IdHomologacion": "43", "Data": "43 idHomologacion de dato"},
    {"IdHomologacion": "44", "Data": "44 idHomologacion de dato"},
    {"IdHomologacion": "46", "Data": "46 idHomologacion de dato"}
]', CAST(N'2024-06-03T22:22:02.100' AS DateTime), CAST(N'2024-06-03T22:22:02.100' AS DateTime), N'A')
GO
INSERT [dbo].[DataLakeOrganizacion] ([IdDataLakeOrganizacion], [IdHomologacionEsquema], [IdDataLake], [DataEsquemaJson], [DataFechaCarga], [FechaCreacion], [Estado]) VALUES (7, 3, 5, N'[
    {"IdHomologacion": "41", "Data": "41 idHomologacion de dato"},
    {"IdHomologacion": "42", "Data": "42 idHomologacion de dato"},
    {"IdHomologacion": "43", "Data": "43 idHomologacion de dato"}]', CAST(N'2024-06-03T22:22:10.770' AS DateTime), CAST(N'2024-06-03T22:22:10.770' AS DateTime), N'A')
GO
INSERT [dbo].[DataLakeOrganizacion] ([IdDataLakeOrganizacion], [IdHomologacionEsquema], [IdDataLake], [DataEsquemaJson], [DataFechaCarga], [FechaCreacion], [Estado]) VALUES (8, 3, 6, N'[
    {"IdHomologacion": "41", "Data": "41 idHomologacion de dato"},
    {"IdHomologacion": "42", "Data": "42 idHomologacion de dato"},
    {"IdHomologacion": "43", "Data": "43 idHomologacion de dato"}]', CAST(N'2024-06-03T22:22:18.627' AS DateTime), CAST(N'2024-06-03T22:22:18.627' AS DateTime), N'A')
GO
INSERT [dbo].[DataLakeOrganizacion] ([IdDataLakeOrganizacion], [IdHomologacionEsquema], [IdDataLake], [DataEsquemaJson], [DataFechaCarga], [FechaCreacion], [Estado]) VALUES (9, 4, 5, N'[
    {"IdHomologacion": "41", "Data": "41 idHomologacion de dato"},
    {"IdHomologacion": "42", "Data": "42 idHomologacion de dato"},
    {"IdHomologacion": "43", "Data": "43 idHomologacion de dato"},
    {"IdHomologacion": "44", "Data": "44 idHomologacion de dato"},
    {"IdHomologacion": "46", "Data": "46 idHomologacion de dato"}
]', CAST(N'2024-06-03T22:51:05.657' AS DateTime), CAST(N'2024-06-03T22:51:05.657' AS DateTime), N'A')
GO
INSERT [dbo].[DataLakeOrganizacion] ([IdDataLakeOrganizacion], [IdHomologacionEsquema], [IdDataLake], [DataEsquemaJson], [DataFechaCarga], [FechaCreacion], [Estado]) VALUES (10, 4, 6, N'[
    {"IdHomologacion": "41", "Data": "41 idHomologacion de dato"},
    {"IdHomologacion": "42", "Data": "42 idHomologacion de dato"},
    {"IdHomologacion": "43", "Data": "43 idHomologacion de dato"},
    {"IdHomologacion": "44", "Data": "44 idHomologacion de dato"},
    {"IdHomologacion": "46", "Data": "46 idHomologacion de dato"}
]', CAST(N'2024-06-03T22:51:23.837' AS DateTime), CAST(N'2024-06-03T22:51:23.837' AS DateTime), N'A')
GO
SET IDENTITY_INSERT [dbo].[DataLakeOrganizacion] OFF
GO
