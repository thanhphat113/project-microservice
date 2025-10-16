module "global_config" {
  source = "../global-config"
}

module "certificate" {
  source                        = "../certificate"
  domain_name                   = var.domain_name
  create_certificate_for_domain = var.create_certificate_for_domain

  providers = {
    aws = aws.us-east-1
  }
}

module "s3" {
  source = "../s3"

  enable_static_hosting = var.enable_static_hosting
  aws_region            = var.aws_region
  project               = var.project
  env                   = var.env
  block_public_access   = var.block_public_access
}

module "cloud_front" {
  domain_name = var.domain_name
  source      = "../cloud-front"

  bucket_arn          = module.s3.bucket_arn
  bucket_domain_name  = module.s3.bucket_domain_name
  bucket_name         = module.s3.bucket_name
  acm_certificate_arn = module.certificate.aws_certificate_arn

  origin_path = var.origin_path

  aws_region = var.aws_region
  project    = var.project
  env        = var.env
}

module "vpc" {
  source = "../vpc"

  vpc_cidr        = var.vpc_cidr
  public_subnets  = var.public_subnets
  private_subnets = var.private_subnets
  azs             = var.azs
  aws_region      = var.aws_region
  env             = var.env
  project         = var.project

  tags = {
    Enviroment = var.env
    Project    = var.project
  }
}

module "eks" {
  source = "../eks"

  vpc_id             = module.vpc.vpc_id
  subnet_ids         = module.vpc.private_subnets
  max_capacity       = var.max_capacity
  min_capacity       = var.min_capacity
  desired_capacity   = var.desired_capacity
  env                = var.env
  project            = var.project
  aws_region         = var.aws_region
  node_instance_type = var.node_instance_type
}

module "security-group-for-rds" {
  source = "../security-group"

  sg_name = "for-rds"
  ingress_rules = [{
    from_port       = 0
    to_port         = 0
    protocol        = "-1"
    security_groups = module.eks.security_groups_ids
  }]

  vpc_ip     = module.vpc.vpc_id
  env        = var.env
  project    = var.project
  aws_region = var.aws_region
}

module "rds_mysql" {
  source = "../rds"

  aws_region        = var.aws_region
  project           = var.project
  env               = var.env
  db_username       = var.db_username_identity_service
  db_password       = var.db_password_identity_service
  engine            = "mysql"
  engine_version    = "8.0.42"
  allocated_storage = 20
  instance_class    = "db.t4g.micro"
  security_group_id = module.security-group-for-rds.security_group_id
  private_subnets   = module.vpc.private_subnets
}


module "rds_postgresql" {
  source = "../rds"

  aws_region        = var.aws_region
  project           = var.project
  env               = var.env
  db_username       = var.db_username_task_service
  db_password       = var.db_password_task_service
  engine            = "postgres"
  engine_version    = "17.5"
  allocated_storage = 20
  instance_class    = "db.t4g.micro"
  security_group_id = module.security-group-for-rds.security_group_id
  private_subnets   = module.vpc.private_subnets
}

module "alb_certificate" {
  source                        = "../certificate"
  domain_name                   = var.api_domain_name
  create_certificate_for_domain = var.create_certificate_for_domain
}

