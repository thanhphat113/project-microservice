variable "domain_name" {
  description = "The **existing domain name** (e.g., example.com) to be used for this resource."
  type        = list(string)
}

variable "create_certificate_for_domain" {
  type = bool
}
