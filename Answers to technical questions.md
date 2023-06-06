# 1. How long did you spend on the coding assignment? What would you add to your solution if you had more time? 

I spent approximately 50 hours on the coding assignment.
I wanted to incorporate tha last technologies like Polly, Refit, Meiator, etc., which I did not have prior experience with. Familiarizing myself with these technologies took some time.
If I had more time, I would have added these to the solution:
- Adding authorization, 
- Enhancing the test coverage and adding Integrated Tests, 
- Refactoring the codebase to make it more manageable
- Developing an API for managing popular currencies
- Using hooks instead of class components in all React forms. 
- Developing a loading feature to the UI during fetch.



# 2. What was the most useful feature that was added to the latest version of your language of choice? Please include a snippet of code that shows how you've used it.

I personally love the record feature in C#, both because of its positional syntax which makes it very easy to use, and because of its value equality which simplifies comparisons.
I am also happy that primary constructors have been added to the class in version 12.

For example, I have used records extensively in this project, such as defining the GetCurrentQuotesQuery record:
public record GetCurrentQuotesQuery(CurrencySymbol Symbol) : IRequest<IEnumerable<CryptoCurrencyQuoteDto>>
This record represents a request to get the current quotes for a given symbol, and it is very convenient to define and use.



# 3. How would you track down a performance issue in production? Have you ever had to do this?
As an experienced developer, I have frequently addressed performance issues in production environments. My approach typically begins with examining the user interface to confirm that the slowness is specific to a particular request. After this step, I investigate the server to rule out network or firewall issues.

Once I have confirmed that the issue is related to the application or database, I leverage SQL Profiler to identify which SQL queries are causing performance degradation.
If the slowness is caused by the database, I use techniques such as query optimization, temporary table caching, execution plan analysis, index or statistics rebuilding, and modification of stored procedures to improve performance.

If the slowness is related to the application, I focus on improving code quality and complexity. Firstly, I apply Clean Code principles to make the code more readable and maintainable. Then, I analyze the algorithm's complexity and work on optimization if possible. Additionally, I consider implementing caching for frequently called methods or processing large batches of data in parallel to reduce processing time.

And at the end, I thoroughly test any changes before deploying them to production.



# 4. What was the latest technical book you have read or tech conference you have been to?
What did you learn?
The latest tech conference I attended was about various hacking techniques, including phishing. I learned interesting topics about how phishing websites work and how easily they can be set up.



# 5. What do you think about this technical assessment?
Due to the high workload in the company, I rarely have time to work on a new project with new technologies. Therefore, this technical assessment was a good opportunity for me to create time and learn new things, which I always find enjoyable.
However, I had an issue with it because the scope of the project was not clear. It could have been a small 10-hour project with any technology requirements, or it could have been an ongoing project that would require hours of work and still have room for development. For example, I could spend another 50 hours working on it :)



# 6. Please, describe yourself using JSON.
```json
{
  "first_name": "Sadegh",
  "last_name": "Kamalou",
  "gender": "Male",
  "married": true,
  "age": 39,
  "children": [
    {
      "first_name": "Parsa",
      "gender": "Male",
      "marrid": false,
      "age": 6,
      "children": []
    }
  ],
  "work_experience": [
    {
      "company": "Chargoon",
      "position": "Senior Software Engineer",
      "from": 2015,
      "to": 2023
    },
    {
      "company": "Chargoon",
      "position": "Software Engineer",
      "from": 2011,
      "to": 2015
    },
    {
      "company": "Aris",
      "position": "Software Engineer",
      "from": 2009,
      "to": 2011
    }
  ],
  "strengths": [
    "Problem solver",
    "Fast learner",
    "Good team player",
    "Responsible"
  ],
  "educations": [
    {
      "level": "Bachelor",
      "major": "Computer Software Engineering",
      "from": 2003,
      "to": 2007
    }
  ],
  "skills": {
    "technical_skills": [
      "C#",
      "SQL",
      "Javascript"
    ],
    "soft_skills": [
      "Mentoring",
      "Problem Solving",
      "Good team player",
      "Critical Thinking"
    ],
    "principles": [
      "OOP",
      "SOLID",
      "CleanCode"
    ]
  },
  "hobbies": [
    "Walking",
    "Swiming",
    "Fixing things"
  ],
  "contacts": {
    "email": "mr.kamalou@gmail.com",
    "phone": "+98 912 733 52 20"
  }
}
```
