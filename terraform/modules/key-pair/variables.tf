variable "key_name" {
  description = "The name of the key pair to create or reuse"
  type        = string
}

variable "generate_if_missing" {
  description = "If true, generate new key pair when public key not found"
  type        = bool
  default     = true
}

variable "public_key_path" {
  description = "Path to an existing public key file (used when generate_if_missing = false)"
  type        = string
  default     = "~/.ssh/id_rsa.pub"
}

variable "algorithm" {
  description = "Key algorithm to use when generating (RSA or ED25519)"
  type        = string
  default     = "RSA"
}

variable "rsa_bits" {
  description = "RSA key length (only used when algorithm = RSA)"
  type        = number
  default     = 4096
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
