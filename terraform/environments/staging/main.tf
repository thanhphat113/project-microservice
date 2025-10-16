module "main" {
  source = "../../modules/main"

  # global
  aws_profile = var.aws_profile
  aws_region  = var.aws_region
  project     = var.project
  env         = var.env
  domain_name = var.domain_name

  # Certificate
  create_certificate_for_domain = var.create_certificate_for_domain

  # S3
  enable_static_hosting = false
  block_public_access   = true

  # Cloud front
  origin_path = var.origin_path

  # VPC
  vpc_cidr        = var.vpc_cidr
  public_subnets  = var.public_subnets
  private_subnets = var.private_subnets
  azs             = var.azs

  # EKS
  max_capacity       = var.max_capacity
  min_capacity       = var.min_capacity
  desired_capacity   = var.desired_capacity
  node_instance_type = var.node_instance_type

  # Rds for Identity service
  db_password_identity_service = var.db_password_identity_service
  db_username_identity_service = var.db_username_identity_service

  # Rds for Task service
  db_password_task_service = var.db_password_task_service
  db_username_task_service = var.db_username_task_service

  #Certificate for api
  api_domain_name = var.api_domain_name
}
