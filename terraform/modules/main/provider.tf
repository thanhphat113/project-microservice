terraform {
  required_providers {
    aws = {
      source  = "hashicorp/aws"
      version = "~> 6.0"
    }
  }
}


provider "aws" {
  region  = var.aws_region
  profile = var.aws_profile
}

provider "aws" {
  alias  = "us-east-1"
  region = "us-east-1"
}
