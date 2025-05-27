
### CQRS - Command Query Responsibility Segregation
- CQRS is a design pattern used to avoid complex queries and eliminate inefficient joins.
- It separates read and write operations, often utilizing separate databases.
- Commands: Operations that change the state of data within the application.
- Queries: Operations that handle complex join operations and return a result without changing the state of data in the application.
- Large-scale microservices architectures need to manage high-volume data requirements.
- Using a single database for services can cause bottlenecks.
- CQRS often uses both CQRS and Event Sourcing patterns to improve application performance.
- CQRS separates read and write data to maximize query performance and scalability.

# CQRS - Read and Write Operations

Challenges of monolithic database systems for handling both complex queries and CRUD operations, and how CQRS (Command Query Responsibility Segregation) addresses these issues by separating read and write concerns.

## Challenges with Monolithic Databases

* **Single Database Bottleneck:** In a monolithic architecture, a single database is responsible for both complex join queries and CRUD (Create, Read, Update, Delete) operations.
* **Unmanageable Complexity:** As applications grow more complex, the combination of intricate queries and extensive CRUD operations can lead to an unmanageable situation.
* **Database Locking due to Latency:** An application requiring a query that joins more than 10 tables, for example, can cause the database to lock due to the latency of query computation.
* **Locking due to Complex Business Logic:** Performing CRUD operations often necessitates complex validations and processing long business logic, which can also lead to database locking.

## CQRS Solution: Separation of Concerns

CQRS addresses these challenges by applying the "separation of concerns" principle to database operations:

* **Separate Reading and Writing Databases:** CQRS advocates for separating the reading database from the writing database, typically using two distinct databases.
* **Read Database (Query Side):**
    * Often uses **No-SQL databases** with denormalized data.
    * Optimized for fast query performance.
* **Write Database (Command Side):**
    * Typically uses **Relational databases** with fully normalized data.
    * Supports strong data consistency.

By separating these concerns, CQRS aims to improve application performance, scalability, and maintainability, especially for high-volume and complex applications.

# Logical and Physical Implementation of CQRS

Two primary ways to implement Command Query Responsibility Segregation (CQRS): Logical Implementation and Physical Implementation, along with their characteristics and implications.

## Logical Implementation of CQRS

* **Splitting Operations, Not Databases:** In a logical implementation of CQRS, the read (query) operations are separated from the write (command) operations at the **code level**.
* **Same Database, Distinct Paths:** While the same underlying database might be used for both reads and writes, the architectural paths and components responsible for reading and writing data are distinct. This means your application code clearly delineates between logic for modifying data and logic for retrieving data.

## Physical Implementation of CQRS

* **Separate Databases:** A physical implementation of CQRS involves splitting the read and write operations not just at the code level but also **physically using separate databases**.
* **Data Consistency and Synchronization:** This approach inherently introduces challenges related to data consistency and synchronization between the write and read databases. Typically, an eventual consistency model is adopted, where changes to the write database are asynchronously propagated to the read database.

## CQRS Design Pattern Diagram

```mermaid
graph LR
    A[Client] --> B(UI)
    B --> C{Command}
    C --> D[Write Database (Tables)]
    D -- |Eventual Consistency| --> E[Read Database (Materialized View)]
    B --> F{Query}
    F --> E