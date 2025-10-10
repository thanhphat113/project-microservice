data "aws_caller_identity" "current" {}

module "global_config" {
  source = "../../modules/global-config"
}

module "vpc" {
  source = "../../modules/vpc"

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
