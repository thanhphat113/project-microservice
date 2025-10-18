vpc_cidr = "10.0.0.0/16"

domain_name     = ["todolist.x10.network", "www.todolist.x10.network"]
api_domain_name = ["api.todolist.x10.network", "www.api.todolist.x10.network"]

public_subnets = [
  "10.0.1.0/24",
  "10.0.2.0/24"
]

private_subnets = [
  "10.0.3.0/24",
  "10.0.4.0/24"
]

azs = [
  "ap-southeast-1a",
  "ap-southeast-1b"
]

env = "staging"

node_instance_type = "t4g.small"

desired_capacity = 1
max_capacity     = 2
min_capacity     = 1

origin_path = "/dist"

project    = "todolist"
aws_region = "ap-southeast-1"
