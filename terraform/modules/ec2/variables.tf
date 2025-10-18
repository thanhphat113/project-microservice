variable "ami_id" {
  description = "The Amazon Machine Image (AMI) ID to use for the EC2 instance"
  type        = string
}

variable "instance_type" {
  description = "The type of EC2 instance (e.g., t3.micro, t3.medium)"
  type        = string
  default     = "t3.micro"
}

variable "subnet_id" {
  description = "The ID of the subnet where the EC2 instance will be deployed"
  type        = string
}

variable "security_group_ids" {
  description = "A list of security group IDs to associate with the EC2 instance"
  type        = list(string)
}

variable "key_name" {
  description = "The name of the SSH key pair to connect to the instance"
  type        = string
}

variable "tags" {
  description = "A map of tags to assign to the EC2 instance"
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
