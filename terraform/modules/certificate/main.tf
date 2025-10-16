data "aws_acm_certificate" "existing" {
  count = var.create_certificate_for_domain ? 0 : 1

  domain      = var.domain_name[0]
  statuses    = ["ISSUED"]
  most_recent = true
}


resource "aws_acm_certificate" "this" {
  count                     = var.create_certificate_for_domain ? 1 : 0
  domain_name               = var.domain_name[0]
  subject_alternative_names = slice(var.domain_name, 1, length((var.domain_name)))
  validation_method         = "DNS"

  lifecycle {
    create_before_destroy = true
  }
}
