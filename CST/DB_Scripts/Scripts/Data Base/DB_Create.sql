/****** Object:  Table [dbo].[SolutionFrameworkRole]    Script Date: 08/30/2012 09:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionFrameworkRole](
	[roleid] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[isglobal] [bit] NOT NULL,
	[inserttimestamp] [datetime] NOT NULL,
	[updatetimestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_SolutionFramework_Role] PRIMARY KEY CLUSTERED 
(
	[roleid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolutionFrameworkRight]    Script Date: 08/30/2012 09:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionFrameworkRight](
	[rightid] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](50) NOT NULL,
	[description] [nvarchar](255) NULL,
	[inserttimestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_SolutionFramework_Right] PRIMARY KEY CLUSTERED 
(
	[rightid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolutionFrameWorkNodeAplication]    Script Date: 08/30/2012 09:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionFrameWorkNodeAplication](
	[nodeAplicationId] [int] IDENTITY(1,1) NOT NULL,
	[name] [nvarchar](256) NOT NULL,
 CONSTRAINT [PK_SolutionFrameWorkNodeAplication] PRIMARY KEY CLUSTERED 
(
	[nodeAplicationId] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolutionFrameworkUser]    Script Date: 08/30/2012 09:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SolutionFrameworkUser](
	[userid] [int] IDENTITY(1,1) NOT NULL,
	[username] [nvarchar](50) NOT NULL,
	[password] [nvarchar](100) NOT NULL,
	[firstname] [nvarchar](100) NULL,
	[lastname] [nvarchar](100) NULL,
	[email] [nvarchar](100) NOT NULL,
	[CompleteName] [varchar](200) NULL,
	[timezone] [int] NOT NULL,
	[isactive] [bit] NULL,
	[lastlogin] [datetime] NULL,
	[lastip] [nvarchar](40) NULL,
	[inserttimestamp] [datetime] NOT NULL,
	[updatetimestamp] [datetime] NOT NULL,
 CONSTRAINT [PK_SolutionFramework_User] PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SolutionFrameworkUserRole]    Script Date: 08/30/2012 09:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionFrameworkUserRole](
	[userid] [int] NOT NULL,
	[roleid] [int] NOT NULL,
 CONSTRAINT [PK_SolutionFrameworkUserRole_1] PRIMARY KEY CLUSTERED 
(
	[userid] ASC,
	[roleid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolutionFrameworkRoleRight]    Script Date: 08/30/2012 09:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionFrameworkRoleRight](
	[roleid] [int] NOT NULL,
	[rightid] [int] NOT NULL,
 CONSTRAINT [PK_SolutionFramework_RoleRight] PRIMARY KEY CLUSTERED 
(
	[roleid] ASC,
	[rightid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SolutionFrameworkNode]    Script Date: 08/30/2012 09:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[SolutionFrameworkNode](
	[nodeid] [int] IDENTITY(1,1) NOT NULL,
	[parentnodeid] [int] NULL,
	[title] [nvarchar](255) NOT NULL,
	[shortdescription] [nvarchar](255) NOT NULL,
	[position] [int] NOT NULL,
	[culture] [nvarchar](8) NOT NULL,
	[showinnavigation] [bit] NOT NULL,
	[IsActive] [bit] NOT NULL,
	[linkurl] [nvarchar](255) NULL,
	[icono] [varchar](500) NULL,
	[inserttimestamp] [datetime] NOT NULL,
	[updatetimestamp] [datetime] NOT NULL,
	[nodeAplicationId] [int] NOT NULL,
 CONSTRAINT [PK_SolutionFramework_Node] PRIMARY KEY CLUSTERED 
(
	[nodeid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[SolutionFrameworkNodeRole]    Script Date: 08/30/2012 09:25:16 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SolutionFrameworkNodeRole](
	[nodeid] [int] NOT NULL,
	[roleid] [int] NOT NULL,
 CONSTRAINT [PK_SolutionFrameworkNodeRole] PRIMARY KEY CLUSTERED 
(
	[nodeid] ASC,
	[roleid] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_SolutionFramework_Node_position]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkNode] ADD  CONSTRAINT [DF_SolutionFramework_Node_position]  DEFAULT ((0)) FOR [position]
GO
/****** Object:  Default [DF_SolutionFramework_Node_inserttimestamp]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkNode] ADD  CONSTRAINT [DF_SolutionFramework_Node_inserttimestamp]  DEFAULT (getdate()) FOR [inserttimestamp]
GO
/****** Object:  Default [DF_SolutionFramework_Node_updatetimestamp]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkNode] ADD  CONSTRAINT [DF_SolutionFramework_Node_updatetimestamp]  DEFAULT (getdate()) FOR [updatetimestamp]
GO
/****** Object:  Default [DF_SolutionFramework_Right_inserttimestamp]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkRight] ADD  CONSTRAINT [DF_SolutionFramework_Right_inserttimestamp]  DEFAULT (getdate()) FOR [inserttimestamp]
GO
/****** Object:  Default [DF_SolutionFramework_Role_isglobal]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkRole] ADD  CONSTRAINT [DF_SolutionFramework_Role_isglobal]  DEFAULT ((1)) FOR [isglobal]
GO
/****** Object:  Default [DF_SolutionFramework_Role_inserttimestamp]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkRole] ADD  CONSTRAINT [DF_SolutionFramework_Role_inserttimestamp]  DEFAULT (getdate()) FOR [inserttimestamp]
GO
/****** Object:  Default [DF_SolutionFramework_Role_updatetimestamp]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkRole] ADD  CONSTRAINT [DF_SolutionFramework_Role_updatetimestamp]  DEFAULT (getdate()) FOR [updatetimestamp]
GO
/****** Object:  Default [DF_SolutionFramework_User_timezone]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkUser] ADD  CONSTRAINT [DF_SolutionFramework_User_timezone]  DEFAULT ((0)) FOR [timezone]
GO
/****** Object:  Default [DF_SolutionFramework_User_inserttimestamp]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkUser] ADD  CONSTRAINT [DF_SolutionFramework_User_inserttimestamp]  DEFAULT (getdate()) FOR [inserttimestamp]
GO
/****** Object:  Default [DF_SolutionFramework_User_updatetimestamp]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkUser] ADD  CONSTRAINT [DF_SolutionFramework_User_updatetimestamp]  DEFAULT (getdate()) FOR [updatetimestamp]
GO
/****** Object:  ForeignKey [FK_SolutionFrameworkNode_SolutionFrameWorkNodeAplication]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkNode]  WITH CHECK ADD  CONSTRAINT [FK_SolutionFrameworkNode_SolutionFrameWorkNodeAplication] FOREIGN KEY([nodeAplicationId])
REFERENCES [dbo].[SolutionFrameWorkNodeAplication] ([nodeAplicationId])
GO
ALTER TABLE [dbo].[SolutionFrameworkNode] CHECK CONSTRAINT [FK_SolutionFrameworkNode_SolutionFrameWorkNodeAplication]
GO
/****** Object:  ForeignKey [FK_SolutionFramework_NodeRole_SolutionFramework_Node]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkNodeRole]  WITH CHECK ADD  CONSTRAINT [FK_SolutionFramework_NodeRole_SolutionFramework_Node] FOREIGN KEY([nodeid])
REFERENCES [dbo].[SolutionFrameworkNode] ([nodeid])
GO
ALTER TABLE [dbo].[SolutionFrameworkNodeRole] CHECK CONSTRAINT [FK_SolutionFramework_NodeRole_SolutionFramework_Node]
GO
/****** Object:  ForeignKey [FK_SolutionFramework_NodeRole_SolutionFramework_Role]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkNodeRole]  WITH CHECK ADD  CONSTRAINT [FK_SolutionFramework_NodeRole_SolutionFramework_Role] FOREIGN KEY([roleid])
REFERENCES [dbo].[SolutionFrameworkRole] ([roleid])
GO
ALTER TABLE [dbo].[SolutionFrameworkNodeRole] CHECK CONSTRAINT [FK_SolutionFramework_NodeRole_SolutionFramework_Role]
GO
/****** Object:  ForeignKey [FK_SolutionFramework_RoleRight_SolutionFramework_Right]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkRoleRight]  WITH CHECK ADD  CONSTRAINT [FK_SolutionFramework_RoleRight_SolutionFramework_Right] FOREIGN KEY([rightid])
REFERENCES [dbo].[SolutionFrameworkRight] ([rightid])
GO
ALTER TABLE [dbo].[SolutionFrameworkRoleRight] CHECK CONSTRAINT [FK_SolutionFramework_RoleRight_SolutionFramework_Right]
GO
/****** Object:  ForeignKey [FK_SolutionFramework_RoleRight_SolutionFramework_Role]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkRoleRight]  WITH CHECK ADD  CONSTRAINT [FK_SolutionFramework_RoleRight_SolutionFramework_Role] FOREIGN KEY([roleid])
REFERENCES [dbo].[SolutionFrameworkRole] ([roleid])
GO
ALTER TABLE [dbo].[SolutionFrameworkRoleRight] CHECK CONSTRAINT [FK_SolutionFramework_RoleRight_SolutionFramework_Role]
GO
/****** Object:  ForeignKey [FK_SolutionFramework_UserRole_SolutionFramework_Role]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkUserRole]  WITH CHECK ADD  CONSTRAINT [FK_SolutionFramework_UserRole_SolutionFramework_Role] FOREIGN KEY([roleid])
REFERENCES [dbo].[SolutionFrameworkRole] ([roleid])
GO
ALTER TABLE [dbo].[SolutionFrameworkUserRole] CHECK CONSTRAINT [FK_SolutionFramework_UserRole_SolutionFramework_Role]
GO
/****** Object:  ForeignKey [FK_SolutionFramework_UserRole_SolutionFramework_User]    Script Date: 08/30/2012 09:25:16 ******/
ALTER TABLE [dbo].[SolutionFrameworkUserRole]  WITH CHECK ADD  CONSTRAINT [FK_SolutionFramework_UserRole_SolutionFramework_User] FOREIGN KEY([userid])
REFERENCES [dbo].[SolutionFrameworkUser] ([userid])
GO
ALTER TABLE [dbo].[SolutionFrameworkUserRole] CHECK CONSTRAINT [FK_SolutionFramework_UserRole_SolutionFramework_User]
GO
