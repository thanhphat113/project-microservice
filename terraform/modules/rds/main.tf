resource "aws_db_subnet_group" "this" {
  subnet_ids = var.private_subnets

  tags = merge(var.tags, { Name = "${var.project}-db-subnet-group-${var.env}-${var.aws_region}" })
}

resource "aws_db_instance" "this" {
  identifier             = "${var.project}-${var.engine}-${var.env}-${var.aws_region}"
  engine                 = var.engine
  engine_version         = var.engine_version
  username               = var.db_username
  password               = var.db_password
  instance_class         = var.instance_class
  allocated_storage      = var.allocated_storage
  db_subnet_group_name   = aws_db_subnet_group.this.name
  vpc_security_group_ids = [var.security_group_id]

  skip_final_snapshot = true
  publicly_accessible = false
  storage_encrypted   = true

  lifecycle {
    ignore_changes = [
      username,
      password
    ]
  }

  tags = merge(var.tags, {
    Name = "${var.project}-${var.engine}-${var.env}-${var.aws_region}"
  })

}
