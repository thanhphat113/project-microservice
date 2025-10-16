output "bucket_name" {
  value = aws_s3_bucket.this.id
}

output "bucket_domain_name" {
  value = aws_s3_bucket.this.bucket_domain_name
}

output "bucket_arn" {
  value = aws_s3_bucket.this.arn
}

output "website_endpoint" {
  value = var.enable_static_hosting ? aws_s3_bucket_website_configuration.this[0].website_endpoint : null
}
