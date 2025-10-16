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

variable "bucket_domain_name" {
  type = string
}

variable "default_root_object" {
  type    = string
  default = "index.html"
}

variable "origin_path" {
  type = string
}

variable "price_class" {
  description = "Price class for CloudFront"
  type        = string
  default     = "PriceClass_200"
}

variable "bucket_name" {
  type = string
}

variable "bucket_arn" {
  type = string
}

variable "domain_name" {
  description = "The **existing domain name** (e.g., example.com) to be used for this resource."
  type        = list(string)
}

variable "acm_certificate_arn" {
  type = string
}
