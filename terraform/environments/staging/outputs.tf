output "identity_connection_string" {
  value     = module.main.identity_connection_string
  sensitive = true
}

output "task_connection_url" {
  value = module.main.task_connection_url
}

output "task_db_username" {
  value     = module.main.task_db_username
  sensitive = true
}

output "task_db_password" {
  value     = module.main.task_db_password
  sensitive = true
}

output "cloud_front_domain_name" {
  value = module.main.domain_name
}

output "certificate_alb" {
  value = module.main.certificate_alb
}

output "eks-name" {
  value = module.main.eks_name
}

output "aws_region" {
  value = var.aws_region
}

output "profile_name" {
  value = var.aws_profile
}
