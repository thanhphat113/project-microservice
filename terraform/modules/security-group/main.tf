resource "aws_security_group" "this" {
  name   = "${var.project}-sg-${var.sg_name}-${var.env}-${var.aws_region}"
  vpc_id = var.vpc_ip

  dynamic "ingress" {
    for_each = var.ingress_rules

    content {
      from_port = ingress.value.from_port
      to_port   = ingress.value.to_port
      protocol  = ingress.value.protocol

      cidr_blocks      = lookup(ingress.value, "cidr_blocks", null)
      ipv6_cidr_blocks = lookup(ingress.value, "ipv6_cidr_blocks", null)
      security_groups  = lookup(ingress.value, "security_groups", null)
    }
  }

  dynamic "egress" {
    for_each = var.egress_rules
    content {
      from_port   = egress.value.from_port
      to_port     = egress.value.to_port
      protocol    = egress.value.protocol
      cidr_blocks = egress.value.cidr_blocks
    }
  }


  tags = merge(var.tags)
}
