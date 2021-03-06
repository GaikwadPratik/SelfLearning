USE [HappyHutDataBase]
GO
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'A9066CFF-75A8-4EFB-A5FB-FBBF5D8E9AAA', N'ADMIN')
INSERT [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'A91EA8DF-6869-4E19-830E-1E2465C9406F', N'SERVICEREQUESTUSER')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'2ccdd404-1acc-4638-a80d-72000f8736be', N'Pratik.gaikwad19@gmail.com', 0, N'AEjvp2AsCbVWbzVKirCnMdzZCYQV2i8Y/O1UyTmJK2/DuxeSl7qDTEqZ8jGd3bYZRQ==', N'd0a02f0c-0d9b-4713-9fb6-c5b48e0b5eb5', NULL, 0, 0, NULL, 0, 0, N'Pratik.gaikwad19@gmail.com')
INSERT [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'4b002f68-a683-4d69-9e3b-a058e7fbd944', N'sgandewar@gmail.com', 0, N'ALbD+WgS8e6kFxtS6F+CB0PrCveOHyya3qQ/OPiDR2/13OWTLWCKzkKiU0PkTyjkbg==', N'a494618d-6a3d-4a38-9d77-4eb30d0cf674', NULL, 0, 0, NULL, 0, 0, N'sgandewar@gmail.com')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'2ccdd404-1acc-4638-a80d-72000f8736be', N'A91EA8DF-6869-4E19-830E-1E2465C9406F')
INSERT [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'4b002f68-a683-4d69-9e3b-a058e7fbd944', N'A91EA8DF-6869-4E19-830E-1E2465C9406F')
SET IDENTITY_INSERT [dbo].[States] ON 

INSERT [dbo].[States] ([Id], [Name]) VALUES (1, N'Maharashtra')
INSERT [dbo].[States] ([Id], [Name]) VALUES (2, N'Andhra Pradesh')
INSERT [dbo].[States] ([Id], [Name]) VALUES (3, N'Delhi')
INSERT [dbo].[States] ([Id], [Name]) VALUES (4, N'Tamil Nadu')
SET IDENTITY_INSERT [dbo].[States] OFF
INSERT [dbo].[Cities] ([ID], [Name], [StateId]) VALUES (1, N'Mumbai', 1)
INSERT [dbo].[Cities] ([ID], [Name], [StateId]) VALUES (2, N'Hyderabad', 2)
INSERT [dbo].[Cities] ([ID], [Name], [StateId]) VALUES (3, N'Pune', 1)
INSERT [dbo].[Cities] ([ID], [Name], [StateId]) VALUES (4, N'Delhi', 3)
INSERT [dbo].[Cities] ([ID], [Name], [StateId]) VALUES (5, N'Chennai', 4)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (1, N'Bhandup', 1)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (2, N'Powai', 1)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (3, N'Miyapur', 2)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (4, N'Thane', 1)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (5, N'Kondapur', 2)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (6, N'Secundrabad', 2)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (7, N'Chandanagar', 2)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (8, N'Gachibowli', 2)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (9, N'Kurla', 1)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (10, N'Ghatkopar', 1)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (11, N'Vikhroli', 1)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (12, N'Dombivali', 1)
INSERT [dbo].[AreasInCities] ([ID], [Name], [CityID]) VALUES (13, N'Kalyan', 1)
SET IDENTITY_INSERT [dbo].[Services] ON 

INSERT [dbo].[Services] ([Id], [Name]) VALUES (1, N'Electrician')
INSERT [dbo].[Services] ([Id], [Name]) VALUES (2, N'Pest Control')
INSERT [dbo].[Services] ([Id], [Name]) VALUES (3, N'Plumber')
SET IDENTITY_INSERT [dbo].[Services] OFF
SET IDENTITY_INSERT [dbo].[ServicesInAreas] ON 

INSERT [dbo].[ServicesInAreas] ([Id], [ServiceID], [AreaID], [IsActive]) VALUES (1, 2, 3, 1)
INSERT [dbo].[ServicesInAreas] ([Id], [ServiceID], [AreaID], [IsActive]) VALUES (2, 2, 6, 1)
INSERT [dbo].[ServicesInAreas] ([Id], [ServiceID], [AreaID], [IsActive]) VALUES (3, 2, 7, 1)
INSERT [dbo].[ServicesInAreas] ([Id], [ServiceID], [AreaID], [IsActive]) VALUES (4, 3, 1, 1)
INSERT [dbo].[ServicesInAreas] ([Id], [ServiceID], [AreaID], [IsActive]) VALUES (5, 3, 2, 1)
INSERT [dbo].[ServicesInAreas] ([Id], [ServiceID], [AreaID], [IsActive]) VALUES (6, 1, 9, 1)
INSERT [dbo].[ServicesInAreas] ([Id], [ServiceID], [AreaID], [IsActive]) VALUES (7, 1, 10, 1)
INSERT [dbo].[ServicesInAreas] ([Id], [ServiceID], [AreaID], [IsActive]) VALUES (8, 1, 11, 1)
INSERT [dbo].[ServicesInAreas] ([Id], [ServiceID], [AreaID], [IsActive]) VALUES (9, 1, 12, 1)
INSERT [dbo].[ServicesInAreas] ([Id], [ServiceID], [AreaID], [IsActive]) VALUES (10, 1, 13, 1)
INSERT [dbo].[ServicesInAreas] ([Id], [ServiceID], [AreaID], [IsActive]) VALUES (11, 1, 1, 1)
SET IDENTITY_INSERT [dbo].[ServicesInAreas] OFF
