Create database OrientalMedicalSystemDB
use OrientalMedicalSystemDB


CREATE TABLE Personal
(
    PersonalID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Nombre varchar(50) NOT NULL,
	Apellido varchar(50) NOT NULL,
	Cedula varchar(11) NOT NULL,
	Ocupacion varchar(50) NOT NULL,
	DoctorID int NULL,
	IsActive bit not null,
	Constraint Fk_Doctor_Asistente FOREIGN KEY(DoctorID)
    REFERENCES Personal(PersonalID) ON DELETE NO ACTION,
)

Create Table Ciencias
(
    CienciaId int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	Ciencia varchar(50) NOT NULL,
	IsActive bit not null
)

CREATE TABLE Especialidad(
	EspecialidadId int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	DoctorID int NOT NULL,
	CienciaId int Not NULL,
	IsActive bit not null,
	Constraint Fk_Especialidad_Doctor FOREIGN KEY(DoctorID)
    REFERENCES Personal(PersonalID) ON DELETE NO ACTION,
	Constraint Fk_Especialidad_Ciencia FOREIGN KEY(CienciaId)
    REFERENCES Ciencias(CienciaId) ON DELETE NO ACTION
)

Create Table Ausencia(
   AusenciaID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
   DoctorID int NOT NULL,
   MotivoAusencia varchar(50) not null,
   fechaInicio datetime not null,
   FechaReintegro datetime not null,
   IsActive bit not null,
   Constraint Fk_Ausencia_Doctor FOREIGN KEY(DoctorID)
   REFERENCES Personal(PersonalID) ON DELETE NO ACTION,
)


CREATE TABLE Usuarios(
   UsuarioID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
   PersonalID int Not NULL,
   Usuario varchar(50) NOT NULL,
   Clave varchar(50) NOT NULL,
   Estado int not null,
   IsActive bit not null,
   Constraint Fk_Usuarios_Personal FOREIGN KEY(PersonalID)
   REFERENCES Personal(PersonalID) ON DELETE NO ACTION,
)

CREATE TABLE Paciente(
   PacienteID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
   Nombre varchar(50) NOT NULL,
   Apellido varchar(50) NOT NULL,
   Cedula varchar(11) NOT NULL,
   Telefono varchar(10) NOT NULL,
   AsistenteId int not Null,
   IsActive bit not null
   Constraint Fk_Paciente_Asistente FOREIGN KEY(AsistenteId)
   REFERENCES Personal(PersonalID) ON DELETE NO ACTION,
)

CREATE TABLE Citas(
	 CitaID int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	 FechaCita datetime not null,
	 EspecialidadId int NOT NULL,
	 DoctorID int NOT NULL,
	 PacienteID int NOT NULL,
	 Estado int NOT NULL,
	 Comentario varchar(100) null,
	 IsActive bit not null,
	 Constraint Fk_Cita_Doctor FOREIGN KEY(DoctorID)
     REFERENCES Personal(PersonalID) ON DELETE NO ACTION,
	 Constraint Fk_Cita_Especialidad FOREIGN KEY(EspecialidadId)
     REFERENCES Especialidad(EspecialidadId) ON DELETE NO ACTION,
	 Constraint Fk_Cita_Paciente FOREIGN KEY(PacienteID)
     REFERENCES Paciente(PacienteID) ON DELETE NO ACTION
)

Create Table Horario
(
    HorarioId int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	DoctorId int not null,
	HoraInicio varchar(11) not null,
	HoraFin varchar(11) not null,
	MinutosPorPaciente varchar(11) not null,
	IsActive bit not null,
	Constraint Fk_Horario_Doctor FOREIGN KEY(DoctorID)
    REFERENCES Personal(PersonalID) ON DELETE NO ACTION,
)

Create Table DiasLaborables
(
    DiasLaborablesId int PRIMARY KEY IDENTITY(1,1) NOT NULL,
	HorarioId int not null,
	Lunes bit null,
	Martes bit null,
	Miecoles bit null,
	Jueves bit null,
	Viernes bit null,
	Sabado bit null,
	IsActive bit not null,
	Constraint Fk_DiasLaborables_Horario FOREIGN KEY(HorarioId)
    REFERENCES Horario(HorarioId) ON DELETE NO ACTION,
)