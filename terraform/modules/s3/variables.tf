variable "env" {
  type = string
}

variable "project" {
  type = string
}

variable "aws_region" {
  type = string
}

variable "tags" {
  description = "A map of tags to assign to the Security Group"
  type        = map(string)
  default     = {}
}

variable "index_document" {
  type    = string
  default = null
}

variable "error_document" {
  type    = string
  default = null
}

variable "block_public_access" {
  type = bool
}

variable "enable_static_hosting" {
  type    = bool
  default = false
}
