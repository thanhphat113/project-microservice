output "identity_connection_string" {
  value     = "Server=${module.rds_mysql.db_endpoint};Database=identity_db;User Id=${var.db_username_identity_service};Password=${var.db_password_identity_service};"
  sensitive = true
}

output "task_connection_url" {
  value = "jdbc:postgresql://${module.rds_postgresql.db_endpoint}:5432/task_db"
}

output "task_db_username" {
  value     = var.db_username_task_service
  sensitive = true
}

output "task_db_password" {
  value     = var.db_password_task_service
  sensitive = true
}

output "domain_name" {
  value = module.cloud_front.domain_name
}

output "certificate_alb" {
  value = module.alb_certificate.aws_certificate_arn
}

output "eks_name" {
  value = module.eks.cluster_name
}


