import * as fs from "fs";
import * as path from "path";
import sass from "sass";

const getComponents = () => {
  let allComponents: { input: string; output: string }[] = [];

  const types = ["components"];

  types.forEach((type) => {
    const allFiles = fs.readdirSync(`src/${type}`).map((file) => ({
      input: `src/${type}/${file}`,
      output: `lib/${type}/${file.slice(0, -4) + "css"}`,
    }));

    allComponents = [...allComponents, ...allFiles];
  });

  return allComponents;
};

const compile = (filePath: string, fileName: string) => {
  const result = sass.compile(path.resolve(filePath), {
    style: "compressed",
    loadPaths: [path.resolve("src")],
  });

  fs.writeFileSync(path.resolve(fileName), result.css);
  console.log(`wrote ${fileName}`);
};

// Ensure the lib directory exists
try {
    fs.mkdirSync(path.resolve('lib'));
    fs.mkdirSync(path.resolve('lib/components'));
    fs.mkdirSync(path.resolve('lib/themes'));
} catch {}

compile('src/main.scss', 'lib/main.css');
compile('src/pages/calendar.scss', 'lib/calendar.css');

getComponents().forEach((component) => {
  compile(component.input, component.output);
});
