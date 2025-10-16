locals {
  default_cache_behavior = {
    allowed_methods        = ["GET", "HEAD", "OPTIONS", "PUT", "POST", "PATCH", "DELETE"]
    cached_methods         = ["GET", "HEAD"]
    viewer_protocol_policy = "redirect-to-https"
    compress               = true
  }
}

resource "aws_cloudfront_origin_access_control" "this" {
  name                              = "${var.project}-oac-${var.env}-${var.aws_region}"
  origin_access_control_origin_type = "s3"
  signing_behavior                  = "always"
  signing_protocol                  = "sigv4"
}

resource "aws_cloudfront_distribution" "this" {
  aliases             = var.domain_name
  enabled             = true
  default_root_object = var.default_root_object


  origin {
    domain_name = var.bucket_domain_name
    origin_id   = "${var.project}-origin-${var.env}-${var.aws_region}"
    origin_path = var.origin_path

    origin_access_control_id = aws_cloudfront_origin_access_control.this.id
  }

  default_cache_behavior {
    target_origin_id       = "${var.project}-origin-${var.env}-${var.aws_region}"
    viewer_protocol_policy = local.default_cache_behavior.viewer_protocol_policy
    allowed_methods        = local.default_cache_behavior.allowed_methods
    cached_methods         = local.default_cache_behavior.cached_methods

    compress = true

    forwarded_values {
      query_string = false
      cookies {
        forward = "none"
      }
    }
  }

  price_class = var.price_class

  restrictions {
    geo_restriction {
      restriction_type = "none"
    }
  }

  viewer_certificate {
    acm_certificate_arn      = var.acm_certificate_arn
    ssl_support_method       = "sni-only"
    minimum_protocol_version = "TLSv1.2_2021"
  }

  tags = merge(var.tags, { Name = "${var.project}-cloudfront-${var.env}-${var.aws_region}" })
}

resource "aws_s3_bucket_policy" "allowed_cloudfront" {
  bucket = var.bucket_name

  policy = jsonencode(
    {
      Version = "2008-10-17",
      Id      = "PolicyForCloudFrontPrivateContent",
      Statement = [
        {
          Sid    = "AllowCloudFrontServicePrincipal",
          Effect = "Allow",
          Principal = {
            Service = "cloudfront.amazonaws.com"
          },
          Action   = "s3:GetObject",
          Resource = "${var.bucket_arn}/*",
          Condition = {
            ArnLike = {
              "AWS:SourceArn" = "${aws_cloudfront_distribution.this.arn}"
            }
          }
        }
      ]
    }
  )
}
