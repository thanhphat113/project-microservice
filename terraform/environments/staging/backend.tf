terraform {
  cloud {
    organization = "terraform-state-todolist"

    workspaces {
      name = "todolist-staging"
    }
  }

}
