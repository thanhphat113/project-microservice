variable "vpc_id" {
  description = "VPC ID"
  type        = string
}

variable "subnet_ids" {
  description = "List of subnet IDs"
  type        = list(string)
}

variable "node_instance_type" {
  description = "EC2 instance type for nodes"
  type        = string
}

variable "desired_capacity" {
  description = "Desired number of nodes"
  type        = number
}
variable "max_capacity" {
  description = "Max number of nodes"
  type        = number
}

variable "min_capacity" {
  description = "Min number of nodes"
  type        = number
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
