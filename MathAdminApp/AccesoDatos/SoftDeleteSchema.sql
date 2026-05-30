IF COL_LENGTH('dbo.Usuarios', 'activo') IS NULL
    ALTER TABLE dbo.Usuarios ADD activo bit NOT NULL CONSTRAINT DF_Usuarios_activo DEFAULT (1);

IF COL_LENGTH('dbo.Alumnos', 'activo') IS NULL
    ALTER TABLE dbo.Alumnos ADD activo bit NOT NULL CONSTRAINT DF_Alumnos_activo DEFAULT (1);

IF COL_LENGTH('dbo.Tutores', 'activo') IS NULL
    ALTER TABLE dbo.Tutores ADD activo bit NOT NULL CONSTRAINT DF_Tutores_activo DEFAULT (1);

IF COL_LENGTH('dbo.Roles', 'activo') IS NULL
    ALTER TABLE dbo.Roles ADD activo bit NOT NULL CONSTRAINT DF_Roles_activo DEFAULT (1);

IF COL_LENGTH('dbo.CamposFormativos', 'activo') IS NULL
    ALTER TABLE dbo.CamposFormativos ADD activo bit NOT NULL CONSTRAINT DF_CamposFormativos_activo DEFAULT (1);

IF COL_LENGTH('dbo.Proyectos', 'activo') IS NULL
    ALTER TABLE dbo.Proyectos ADD activo bit NOT NULL CONSTRAINT DF_Proyectos_activo DEFAULT (1);

IF COL_LENGTH('dbo.Temas', 'activo') IS NULL
    ALTER TABLE dbo.Temas ADD activo bit NOT NULL CONSTRAINT DF_Temas_activo DEFAULT (1);

IF COL_LENGTH('dbo.Pruebas', 'activo') IS NULL
    ALTER TABLE dbo.Pruebas ADD activo bit NOT NULL CONSTRAINT DF_Pruebas_activo DEFAULT (1);

IF COL_LENGTH('dbo.Preguntas', 'activo') IS NULL
    ALTER TABLE dbo.Preguntas ADD activo bit NOT NULL CONSTRAINT DF_Preguntas_activo DEFAULT (1);

IF COL_LENGTH('dbo.Respuestas', 'activo') IS NULL
    ALTER TABLE dbo.Respuestas ADD activo bit NOT NULL CONSTRAINT DF_Respuestas_activo DEFAULT (1);

IF COL_LENGTH('dbo.Resultados', 'activo') IS NULL
    ALTER TABLE dbo.Resultados ADD activo bit NOT NULL CONSTRAINT DF_Resultados_activo DEFAULT (1);

IF COL_LENGTH('dbo.DocenteAlumno', 'activo') IS NULL
    ALTER TABLE dbo.DocenteAlumno ADD activo bit NOT NULL CONSTRAINT DF_DocenteAlumno_activo DEFAULT (1);
