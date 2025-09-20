// Contenido de terraform/variables.tf

variable "gcp_project_id" {
  description = "El ID del proyecto de Google Cloud."
  type        = string
  default     = "examen-calidad-augusto"
}

variable "gcp_region" {
  description = "La región donde se crearán los recursos."
  type        = string
  default     = "us-central1"
}

variable "database_names" {
  description = "Una lista de nombres para las bases de datos a crear."
  type        = list(string)
  default     = ["calidad_bd_examen"]
}

variable "db_user_name" {
  description = "El nombre de usuario para la base de datos."
  type        = string
  default     = "augustor"
}

variable "db_password" {
  description = "La contraseña para el usuario de la base de datos."
  type        = string
  sensitive   = true
  # ¡SIN VALOR POR DEFECTO! Esto es un secreto y se pasará de forma segura.
}