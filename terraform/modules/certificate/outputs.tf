output "aws_certificate_arn" {
  description = "ARN of ACM certificate for domain"
  value       = var.create_certificate_for_domain ? aws_acm_certificate.this[0].arn : data.aws_acm_certificate.existing[0].arn
}
