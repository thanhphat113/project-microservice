resource "aws_s3_bucket" "this" {
  bucket = "${var.project}-s3-${var.env}-${var.aws_region}"

  force_destroy = true
  tags          = var.tags
}

resource "aws_s3_bucket_public_access_block" "this" {
  bucket = aws_s3_bucket.this.id

  block_public_acls       = var.block_public_access
  block_public_policy     = var.block_public_access
  ignore_public_acls      = var.block_public_access
  restrict_public_buckets = var.block_public_access
}

resource "aws_s3_bucket_website_configuration" "this" {
  count  = var.enable_static_hosting ? 1 : 0
  bucket = aws_s3_bucket.this.id

  dynamic "index_document" {
    for_each = var.index_document != null ? [1] : []

    content {
      suffix = var.index_document
    }
  }

  dynamic "error_document" {
    for_each = var.error_document != null ? [1] : []
    content {
      key = var.error_document
    }


  }
}

data "aws_iam_policy_document" "public_read" {
  count = var.enable_static_hosting && !var.block_public_access ? 1 : 0

  statement {
    actions   = ["s3:GetObject"]
    resources = ["${aws_s3_bucket.this.arn}/*"]

    principals {
      type        = "AWS"
      identifiers = ["*"]
    }

    effect = "Allow"
  }
}

resource "aws_s3_bucket_policy" "this" {
  count  = var.enable_static_hosting && !var.block_public_access ? 1 : 0
  bucket = aws_s3_bucket.this.id
  policy = data.aws_iam_policy_document.public_read[0].json
}

