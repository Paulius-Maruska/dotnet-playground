Task Pipelines
==============

This is my experiment, where I'm trying to figure out how to create pipelines
using the default `System.Threading.Tasks` stuff.

the exercise here is to accomplish this:
- main task, that produces a collection of results.
- processing task, that runs on each element of the collection returned from
  main task.
- finalizing task, that takes a collection of results produced by processing
  tasks.

My Conclusion
=============

I was wishing for something really simple to accomplish this, and I didn't
really like `System.Threading.Tasks` - things are way too complicated.
