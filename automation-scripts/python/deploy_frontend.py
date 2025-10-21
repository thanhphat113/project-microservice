import os
import subprocess
import sys

import boto3

if len(sys.argv) < 2:
    print("Usage: python deploy_frontend.py <S3_BUCKET_NAME>")
    sys.exit(1)

S3_BUCKET_NAME = sys.argv[1]
FRONTEND_DIR = "./Apps/Frontend/Todo-frontend"
BUILD_COMMAND = "npm run build"
BUILD_OUTPUT_DIR = f"{FRONTEND_DIR}/dist"

print("--- Installing Dependencies and Building Frontend ---")
try:
    subprocess.run("npm i", cwd=FRONTEND_DIR, shell=True, check=True)
    
    subprocess.run(BUILD_COMMAND, cwd=FRONTEND_DIR, shell=True, check=True)
    print("Build successful.")
    
except subprocess.CalledProcessError as e:
    print(f"Build process failed: {e}")
    sys.exit(1)
    
if not os.path.exists(BUILD_OUTPUT_DIR):
    print(f"Build output directory not found: {BUILD_OUTPUT_DIR}")
    sys.exit(1)

print(f"--- Syncing files from {BUILD_OUTPUT_DIR} to s3://{S3_BUCKET_NAME} ---")

try:
    aws_sync_command = f"aws s3 sync {BUILD_OUTPUT_DIR} s3://{S3_BUCKET_NAME} --delete"
    subprocess.run(aws_sync_command, shell=True, check=True)
    print("S3 sync successful.")
except subprocess.CalledProcessError as e:
    print(f"S3 sync failed: {e}")
    sys.exit(1)