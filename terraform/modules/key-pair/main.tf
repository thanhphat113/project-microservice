locals {
  pub_key_exists = fileexists(var.public_key_path)
}

resource "tls_private_key" "this" {
  count     = var.generate_if_missing && !local.pub_key_exists ? 1 : 0
  algorithm = var.algorithm
  rsa_bits  = var.algorithm == "RSA" ? var.rsa_bits : null
}

resource "aws_key_pair" "this" {
  key_name   = "${project}-key-pair-${var.env}-${var.aws_region}"
  public_key = local.pub_key_exists ? file(var.public_key_path) : tls_private_key.this[0].pub_key_openssh
}

resource "local_file" "private_key" {
  count           = var.generate_if_missing && !local.pub_key_exists ? 1 : 0
  filename        = "${path.module}/${aws_key_pair.this.key_name}.pem"
  content         = tls_private_key.this[0].private_key_pem
  file_permission = "0600"
}

