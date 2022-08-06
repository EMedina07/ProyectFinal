Create database OrientalMedicalSystemDB
use OrientalMedicalSystemDB


CREATE TABLE Personal
(
    PersonalID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Nombre varchar(50) NOT NULL,
	Apellido varchar(50) NOT NULL,
	Cedula varchar(11) NOT NULL,
	Ocupacion varchar(50) NOT NULL,
	UsuarioCreador varchar(50) Null,
)

CREATE TABLE Especialidad(
	EspecialidadId int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Especialidad varchar(50) NOT NULL,
	DoctorID int NOT NULL,
	AsitenteID int NOT NULL,
	HoraInicio varchar(11),
	HoraFin varchar(11),
	MinutosPorPaciente varchar(11),
	Constraint Fk_Especialidad_Doctor FOREIGN KEY(DoctorID)
    REFERENCES Personal(PersonalID) ON DELETE NO ACTION,
	Constraint Fk_Especialidad_Asistente FOREIGN KEY(AsitenteID)
    REFERENCES Personal(PersonalID) ON DELETE NO ACTION
)

Create Table Ausencia(
   AusenciaID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
   DoctorID int NOT NULL,
   MotivoAusencia varchar(50),
   fechaInicio datetime,
   FechaReintegro datetime,
   Constraint Fk_Ausencia_Doctor FOREIGN KEY(DoctorID)
   REFERENCES Personal(PersonalID) ON DELETE NO ACTION,
)


CREATE TABLE Usuarios(
   UsuarioID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
   PersonalID int Not NULL,
   Usuario varchar(50) NOT NULL,
   Clave varchar(50) NOT NULL,
   Estado int not null,
   Constraint Fk_Usuarios_Personal FOREIGN KEY(PersonalID)
   REFERENCES Personal(PersonalID) ON DELETE NO ACTION,
)

CREATE TABLE Paciente(
   PacienteID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
   Nombre varchar(50) NOT NULL,
   Apellido varchar(50) NOT NULL,
   Cedula varchar(11) NOT NULL,
   Telefono varchar(10) NOT NULL,
   UsuarioCreador varchar(50) not Null,
)

CREATE TABLE Citas(
	 CitaID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	 FechaCita datetime not null,
	 EspecialidadId int NOT NULL,
	 DoctorID int NOT NULL,
	 PacienteID int NOT NULL,
	 Estado int NOT NULL,
	 Comentario varchar(100) null,
	 Constraint Fk_Cita_Doctor FOREIGN KEY(DoctorID)
     REFERENCES Personal(PersonalID) ON DELETE NO ACTION,
	 Constraint Fk_Cita_Especialidad FOREIGN KEY(EspecialidadId)
     REFERENCES Especialidad(EspecialidadId) ON DELETE NO ACTION,
	 Constraint Fk_Cita_Paciente FOREIGN KEY(PacienteID)
     REFERENCES Paciente(PacienteID) ON DELETE NO ACTION,
     Constraint UQ_Citas Unique(EspecialidadId, DoctorID, PacienteID)
)