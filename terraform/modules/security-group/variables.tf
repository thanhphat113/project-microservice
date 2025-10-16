variable "vpc_ip" {
  description = "CIDR block for the VPC"
  type        = string
}

variable "ingress_rules" {
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

variable "egress_rules" {
  type = list(object({
    from_port        = number
    to_port          = number
    protocol         = string
    cidr_blocks      = optional(list(string))
    ipv6_cidr_blocks = optional(list(string))
    security_groups  = optional(list(string))
  }))

  default = [
    {
      from_port   = 0
      to_port     = 0
      protocol    = "-1"
      cidr_blocks = ["0.0.0.0/0"]
    }
  ]
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

variable "tags" {
  description = "A map of tags to assign to the Security Group"
  type        = map(string)
  default     = {}
}

variable "sg_name" {
  type = string
}
