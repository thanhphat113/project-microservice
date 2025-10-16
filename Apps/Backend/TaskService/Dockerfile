FROM maven:3.9.9-eclipse-temurin-21 AS build
WORKDIR /src

COPY pom.xml .
RUN mvn dependency:go-offline -B

COPY src ./src

RUN mvn clean package -DskipTests

FROM eclipse-temurin:21-jre AS final
WORKDIR /app
COPY --from=build /src/target/*.jar app.jar

EXPOSE 9002

ENTRYPOINT [ "java", "-jar", "app.jar" ]
