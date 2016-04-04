#PatientsAndEpisodes task work.

Hello there.

Firstly, I'd like to say i enjoyed this challenge and it took me around two hours to complete.
I was really puzzled by the way the following parts of the task were worded;

- Devise a way to override registrations within the DI container.
- Create a unit test project using NUnit.
- Write unit tests that call the IoC container (with everything pre-registered) to get an instance of the controller, but substituting the 'real' data context for the in-memory one, and verify 

It was odd to me that you weren't mentioning Mocking or anything and was sure it was an oversight. Any how, after a little digging around I understood what you were asking for (or at least I think I did) and proceeded accordingly, it is not an approach I have taken myself before so it was interesting to me.

OK, so I haven't refactored any code other than places then two stub objects into the in-memory db sets from the memory context constructor. I wasn't really sure where else to put them and i did not want to risk tianting the test valididty by recasting the resolved context. I am certain there is a more elegant way of doing it.

There are only 4 tests, one testing each outcome using each context. All should pass.

Given the time;

- I would have refactored quite a bit of the code and created several assemblies (for tidyness).
- I would have used generics and the repository pattern to control crud operations on objects.
- I would have wrapped those in a unit of work.
- I would have removed the logic from the api action and created a service class that privdes the data it needs.
- I would have had more clearly defined tests for each layer (data access, business logic, presentation)

