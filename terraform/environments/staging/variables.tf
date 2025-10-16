variable "project" {
  description = "Project name"
  type        = string
}

variable "aws_region" {
  description = "AWS region to deploy resources"
  type        = string
}

variable "aws_profile" {
  description = "AWS CLI profile name"
  type        = string
  default     = "default"
}

variable "vpc_cidr" {
  type        = string
  description = "CIDR block for the VPC"
}

variable "public_subnets" {
  type        = list(string)
  description = "Public subnet CIDRs"
}

variable "private_subnets" {
  type        = list(string)
  description = "Private subnet CIDRs"
}

variable "azs" {
  type        = list(string)
  description = "Availability Zones"
}

variable "env" {
  description = "Environment name"
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

variable "node_instance_type" {
  description = "EC2 instance type for nodes"
  type        = string
}

variable "ingress_rules_for_rds" {
  type = list(object({
    from_port = number
    to_port   = number
    protocol  = string

    cidr_blocks      = optional(list(string))
    ipv6_cidr_blocks = optional(list(string))
    security_groups  = optional(list(string))
  }))
  default = []
}

variable "origin_path" {
  type = string
}

variable "db_username_identity_service" {
  description = "Username for identity database"
  type        = string
}

variable "db_password_identity_service" {
  description = "Password for identity database (least 8)"
  type        = string
  sensitive   = true
}

variable "db_username_task_service" {
  description = "Username for task database"
  type        = string
}

variable "db_password_task_service" {
  description = "Password for task database (least 8)"
  type        = string
  sensitive   = true
}

variable "domain_name" {
  description = "The **existing domain name** (e.g., example.com) to be used for this resource."
  type        = list(string)
}

variable "api_domain_name" {
  description = "The **existing domain name** (e.g., example.com) to be used for this resource."
  type        = list(string)
}

variable "create_certificate_for_domain" {
  description = "Do you want to create certificate for your domain ?(true or false)"
  type        = bool
}
