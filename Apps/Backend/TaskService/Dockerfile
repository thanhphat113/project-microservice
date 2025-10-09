FROM maven:3.9.9-eclipse-temurin-21 AS build
WORKDIR /src

COPY pom.xml .
RUN mvn dependency:go-offline -B

COPY src ./src

RUN mvn clean package -DskipTests

FROM eclipse-temurin:21-jre AS final
WORKDIR /app
COPY --from=build /src/target/*.jar app.jar

RUN apt-get update && apt-get install -y --no-install-recommends netcat-openbsd && rm -rf /var/lib/apt/lists/*

COPY wait-for-sql.sh /wait-for-sql.sh
RUN chmod +x /wait-for-sql.sh

EXPOSE 9002

ENTRYPOINT ["/wait-for-sql.sh", "java", "-jar", "app.jar" ]
