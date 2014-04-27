require "bundler/setup"
require "albacore"

desc "Build everything"
build :build do |cmd|
  cmd.sln = "dot-net/Centroid.sln"
end

namespace :package do
  desc "Package All"
  task :all => ["package:cs", "package:py", "package:rb"]

  directory "dot-net/build/pkg"
  desc "Package .NET"
  nugets_pack :cs => ["dot-net/build/pkg", :test] do |cmd|
    FileList["dot-net/build/pkg/*.nupkg"].each {|f| File.delete(f) }
    cmd.exe = "dot-net/build/support/NuGet.exe"
    cmd.out = "dot-net/build/pkg"
    cmd.files = ["dot-net/Centroid/Centroid.csproj"]
    cmd.with_metadata do |m|
      m.id = "Centroid"
      m.summary = "A centralized paradigm to configuration management."
      m.description = "Centroid is a tool for loading configuration values declared in JSON, and accessing those configuration values using object properties."
      m.authors = "Resource Data, Inc."
      m.version = "1.0.1"
      m.license_url = "https://github.com/ResourceDataInc/Centroid/blob/master/LICENSE.txt"
      m.project_url = "https://github.com/ResourceDataInc/Centroid"
    end
    cmd.gen_symbols
  end

  desc "Package Python"
  task :py do
    system "cd python && python setup.py sdist"
  end

  desc "Package Ruby"
  task :rb do
    system "git clean *.gem -fx && cd ruby && gem build centroid.gemspec"
  end
end

namespace :release do
  desc "Release All"
  task :all => ["release:cs", "release:py", "release:rb"]

  desc "Release .NET package"
  task :cs  => ["package:cs"] do
    Dir.glob("dot-net/build/pkg/*.nupkg") do |f|
      system "dot-net/build/support/NuGet.exe", ["push", f], :clr_command => true
    end
  end

  desc "Release Python package"
  task :py do
    system "cd python && python setup.py sdist upload"
  end

  desc "Release Ruby package"
  task :rb => ["package:rb"] do
    Dir['ruby/*.gem'].each do |f|
      system "gem push #{f}"
    end
  end
end

namespace :test do
  desc "Test .NET"
  test_runner :cs => [:build] do |cmd|
    cmd.exe = "dot-net/packages/NUnit.Runners.2.6.3/tools/nunit-console.exe"
    cmd.files = ["dot-net/Centroid.Tests/bin/Debug/Centroid.Tests.dll"]
  end

  desc "Test python"
  task :py do
    system "python python/tests.py"
  end

  desc "Test ruby (using TAP)"
  task :rb do
    system "ruby ruby/test/centroid_test.rb --runner tap | node node_modules/tap-parser/example/parse.js"
  end

  desc "Test .NET TAP example"
  task :tap do
    system "dot-net/test/support/tap.exe -s:dot-net/Centroid.Tests/bin/Debug dot-net/Centroid.Tests/hello.cs | node node_modules/tap-parser/example/parse.js"
  end
end

desc "Test everything"
task :test => ["test:cs", "test:py", "test:rb"]

task :default => :test
