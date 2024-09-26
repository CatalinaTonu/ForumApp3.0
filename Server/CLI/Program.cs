using CLI;
using CLI.UI;

using InMemoryRepositories;
using RepositoryContracts;

Console.WriteLine("Starting CLI app...");
// Repositories are instantiated here. Notice this version uses the FileRepositories instead of the InMemoryRepositories. That doesn't matter for you.

IUserRepository userRepository = new UserInMemoryRepository(); // old: UserInMemoryRepository();
ICommentRepository commentRepository = new CommentInMemoryRepository(); // old: CommentInMemoryRepository();
IPostRepository postRepository = new PostInMemoryRepository(); // old:  PostInMemoryRepository();

CliApp cliApp = new CliApp(userRepository, commentRepository, postRepository);
await cliApp.StartAsync(); // everything is async, so we await it.