variable "private_subnets" {
  description = "List of private subnet CIDR blocks"
  type        = list(string)
}

variable "tags" {
  description = "Common tags"
  type        = map(string)
  default     = {}
}

variable "env" {
  description = "Environment name"
  type        = string
}

variable "project" {
  description = "Project name"
  type        = string
}

variable "aws_region" {
  description = "Project name"
  type        = string
}

variable "engine" {
  description = "Database engine (postgres or sqlserver-se)"
  type        = string
}

variable "engine_version" {
  type = string
}

variable "instance_class" {
  description = "Instance class name"
  type        = string
}

variable "allocated_storage" {
  description = "storage"
  type        = string
}

variable "security_group_id" {
  type = string
}

variable "db_username" {
  type = string
}

variable "db_password" {
  type = string
}

