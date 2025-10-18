# DevOps Portfolio

This portfolio showcases my hands-on experience in:

- **Cloud**: AWS (EKS, RDS, S3, etc)
- **Container & Orchestration**: Docker, Kubernetes, Helm
- **Infrastructure as Code (IaC)**: Terraform, Ansible
- **CI/CD**: GitHub Actions, Jenkins
- **Monitoring & Observability**: Prometheus, Grafana
- **Automation / Scripting**: Bash, Python

---

## Projects Overview

1. **Microservice-Demo**  
   - Full-stack microservice deployment (Java + .NET)  
   - Docker Compose for local dev  
   - Kubernetes + Helm for cluster deployment  
   - CI/CD pipelines using GitHub Actions & Jenkins  

2. **Monitoring-Demo**  
   - Prometheus + Grafana setup  
   - ServiceMonitor for scraping metrics  
   - Dashboards and alerts configured  

3. **Automation-Scripts**  
   - Scripts for infrastructure provisioning, backups, health checks

---

## Notes
Due to using AWS Free Tier, some workflows cannot run fully on cloud.  
However, all scripts, manifests, and workflows are fully functional in environments with sufficient resources.

## Project Structure
```
Project-Microservice/
├── Apps
│   ├── Backend                  # Backend microservices (.NET, spring)
│   └── Frontend                 # Frontend (Reactjs)
├── Diagrams                     # Diagrams illustrating workflow & microservice architecture
├── README.md
├── devbox.json                  # Devbox environment configuration
├── docker-compose.dev.yml       # Docker Compose setup for local dev/demo
├── helm                         # Helm charts for Kubernetes deployment
├── .template.env                # Template env for docker compose
└── terraform  
    ├── environments             # Environment-specific configs (staging/prod)
    └── modules                  # Reusable Terraform modules
```

## Setup environment
1. **Install devbox** 
- MacOs
```bash
brew install devbox
```

- Windows (scoop)
```bash
scoop install devbox
```

2. **Check version**
```bash
devbox --version
```

3. **Install dependencies**
```bash
devbox init
devbox shell
```

4. **Setup environment variables**
```bash
cp .template.env .env
```

## 🏃 Run project
1. **Dev environment**
```bash
docker compose -f docker-compose.dev.yml up -d --build
```

2. **Staging environment**
- Login terraform
```bash
terraform login
```

- Setup infrastructure
   - Choose environment you want (staging, production)

```bash
cd ./terraform/environments/<environment>    #!NOTICE: Replace file backend.tf with your Terraform cloud
terraform init
terraform apply
```

- Deploy application (Helm)
```bash
cd ./helm
helm upgrade --install todolist . -f values.<environment>.yaml -n app
#⚠️ Replace <environment> you want (stag, prod)
```




