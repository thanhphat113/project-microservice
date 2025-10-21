#!/bin/bash
if [ "$#" -ne 4 ]; then
  echo "Usage: $0 <DOCKER_USERNAME> <TAG_VERSION_GATEWAY> <TAG_VERSION_TASK> <TAG_VERSION_IDENTITY>"
  exit 1
fi

DOCKER_USERNAME=$1

TAGS=( "$2" "$3" "$4" )
SERVICES=("api-gateway" "task-service" "identity-service")
FOLDER_NAMES=("ApiGateWay","TaskService","IdentityService")

echo "Starting build and push process for services..."

for SERVICE_NAME in "${SERVICES[@]}"; do
  SERVICE_NAME=${SERVICES[i]}
  SERVICE_TAG=${TAGS[i]}
  FOLDER_NAME=${FOLDER_NAMES[i]}
  
  CONTEXT_PATH="./Apps/Backend/$FOLDER_NAME"
  DOCKERFILE_PATH="./Apps/Backend/$FOLDER_NAME/Dockerfile"

  FULL_TAG="$DOCKER_USERNAME/$REPO_NAME:$SERVICE_TAG"
  
  echo "--- Building and pushing $SERVICE_NAME with tag $FULL_TAG ---"
  
  docker build \
    -t "$FULL_TAG" \
    -f "$DOCKERFILE_PATH" \
    "$CONTEXT_PATH"
  
  if [ $? -ne 0 ]; then
    echo "Docker build failed for $SERVICE_NAME. Exiting."
    exit 1
  fi

  docker push "$FULL_TAG"
  
  if [ $? -ne 0 ]; then
    echo "Docker push failed for $SERVICE_NAME. Exiting."
    exit 1
  fi
  
  echo "$SERVICE_NAME built and pushed successfully."

done

echo "All services built and pushed successfully."